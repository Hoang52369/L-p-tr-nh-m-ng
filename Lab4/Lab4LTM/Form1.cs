using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Lab4LTM
{
    public partial class Form1 : Form
    {
        private static List<Socket> _clients = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] _buffer = new byte[1024];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            SetupServer();
            btnStartServer.Enabled = false;
        }

        private void SetupServer()
        {
            int port = 100;
            _serverSocket.Bind(new IPEndPoint(IPAddress.Any, port));
            _serverSocket.Listen(5);
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);

            // In địa chỉ IP và cổng của máy chủ
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    MessageBox.Show("Server IP: " + ip.ToString() + ", Port: " + port);
                }
            }
        }

        private class SocketState
        {
            public Socket ClientSocket { get; set; }
            public byte[] Buffer { get; set; }

            public SocketState(Socket clientSocket)
            {
                ClientSocket = clientSocket;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket clientSocket = _serverSocket.EndAccept(ar);
            _clients.Add(clientSocket);

            SocketState state = new SocketState(clientSocket);
            byte[] buffer = new byte[1024];
            state.Buffer = buffer;

            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);

            IPEndPoint clientEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
            string clientIP = clientEndPoint.Address.ToString();
            int clientPort = clientEndPoint.Port;
            AppendMessage("Client " + clientIP + ":" + clientPort + " đã kết nối!");
            BroadcastMessage(clientSocket, "Client " + clientIP + ":" + clientPort + " đã kết nối!");

            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private void BroadcastMessage(Socket sender, string message)
        {
            foreach (Socket client in _clients)
            {
                if (client != sender)
                {
                    try
                    {
                        client.Send(Encoding.UTF8.GetBytes(message));
                    } catch {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        _clients.Remove(client);

                        IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint;
                        string clientIP = clientEndPoint.Address.ToString();
                        int clientPort = clientEndPoint.Port;

                        AppendMessage("Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                        BroadcastMessage(sender, "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                    }
                }
            }
        }

        private void AppendMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(AppendMessage), new object[] { message });
                return;
            }
            txtMessages.AppendText(message + Environment.NewLine);
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void SendMessage()
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                BroadcastMessage(_serverSocket, "Server /➙ " + message);
                AppendMessage("Server ➙ " + message);
                txtMessage.Clear();
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            SocketState state = (SocketState)ar.AsyncState;
            Socket clientSocket = state.ClientSocket;
            int received = 0;

            try
            {
                received = clientSocket.EndReceive(ar);
            }
            catch (SocketException)
            {
                clientSocket.Close();
                _clients.Remove(clientSocket);
                return;
            }

            byte[] dataBuffer = new byte[received];
            Array.Copy(state.Buffer, 0, dataBuffer, 0, received);

            string text = Encoding.UTF8.GetString(dataBuffer);

            IPEndPoint clientEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
            string clientIP = clientEndPoint.Address.ToString();
            int clientPort = clientEndPoint.Port;

            AppendMessage(text);

            if (text.ToLower() == "exit")
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
                _clients.Remove(clientSocket);
                AppendMessage("Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                BroadcastMessage(clientSocket, "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                return;
            }

            BroadcastMessage(clientSocket, text);

            byte[] buffer = new byte[1024];
            state.Buffer = buffer;
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
        }

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendMessage();
            }
        }
    }
}
