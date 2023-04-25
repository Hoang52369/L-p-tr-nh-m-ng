using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4Client
{
    public partial class Form1 : Form
    {
        private Socket _clientSocket;
        private byte[] _buffer = new byte[1024];

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ipAddress = IPAddress.Parse(txtServerIP.Text);
            int port = int.Parse(txtPort.Text);
            IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

            try
            {
                _clientSocket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                _clientSocket.EndConnect(ar);
                MessageBox.Show("Connected to the server!");

                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
                
                IPEndPoint clientEndPoint = _clientSocket.LocalEndPoint as IPEndPoint;
                string clientIP = clientEndPoint.Address.ToString();
                int clientPort = clientEndPoint.Port;

                AppendMessage("Connected to server: " + clientIP + ":" + clientPort);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int received = _clientSocket.EndReceive(ar);
                byte[] dataBuffer = new byte[received];
                Array.Copy(_buffer, dataBuffer, received);

                string text = Encoding.UTF8.GetString(dataBuffer);

                AppendMessage(text);

                _clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnSendMessage_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                IPEndPoint clientEndPoint = _clientSocket.LocalEndPoint as IPEndPoint;
                string clientIP = clientEndPoint.Address.ToString();
                int clientPort = clientEndPoint.Port;

                bool checkServer = false;

                if (clientPort == 100) {
                    checkServer = true;
                }
                
                byte[] data = Encoding.UTF8.GetBytes((checkServer ? "Server" : clientIP + ":" + clientPort) + " ➙ " + message);
                _clientSocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);

                AppendMessage("Bạn ➙ " + message);
                txtMessage.Clear();
            }
        }

        private void SendCallback(IAsyncResult ar)
        {
            _clientSocket.EndSend(ar);
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

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSendMessage_Click(sender, e);
            }
        }
    }
}
