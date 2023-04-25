using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    public partial class Server : Form
    {
        private static List<Socket> _clients = new List<Socket>();
        private static Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] _buffer = new byte[1024];
        public Server()
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

            // Hiển thị danh sách các client đã kết nối
            UpdateConnectedClientsList();

            // Tạo đối tượng state mới cho client
            SocketState state = new SocketState(clientSocket);
            byte[] buffer = new byte[1024];
            state.Buffer = buffer;

            // Bắt đầu nhận dữ liệu từ client
            clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);

            IPEndPoint clientEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
            string clientIP = clientEndPoint.Address.ToString();
            int clientPort = clientEndPoint.Port;
            AppendMessage("Client " + clientIP + ":" + clientPort + " đã kết nối!");
            BroadcastMessage(clientSocket, "Client " + clientIP + ":" + clientPort + " đã kết nối!");

            // Tiếp tục chấp nhận kết nối từ các client khác
            _serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }
        //private delegate void UpdateConnectedClientsListDelegate(Socket client);
        private void UpdateConnectedClientsList()
        {
            // Tạo danh sách tên client kết nối
            List<string> connectedClients = new List<string>();
            foreach (Socket clientSocket in _clients)
            {
                IPEndPoint clientEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
                string clientIP = clientEndPoint.Address.ToString();
                int clientPort = clientEndPoint.Port;
                string clientName = "Client " + clientIP + ":" + clientPort;
                connectedClients.Add(clientName);
            }

            // Hiển thị danh sách trong TextBox
            txtNickName.BeginInvoke(new Action(() =>
            {
                txtNickName.Text = string.Join("\r\n", connectedClients);
            }));
        }
        
        
        private void BroadcastMessage(Socket sender, string message)
        {
            //Vòng lặp foreach để lặp qua tất cả các client kết nối tới server, kiểm tra nếu client không phải
            //là người gửi thông điệp thì thực hiện gửi thông điệp đến client đó.
            foreach (Socket client in _clients)
            {
                if (client != sender)
                {
                    try
                    {
                        //Sử dụng phương thức client.Send để gửi thông điệp đến client.
                        client.Send(Encoding.UTF8.GetBytes(message));
                    }
                    //Nếu có lỗi khi gửi thông điệp đến client thì phương thức sẽ thực hiện loại bỏ client khỏi danh sách _clients,
                    //đồng thời đóng kết nối với client đó.
                    catch
                    {
                        client.Shutdown(SocketShutdown.Both);
                        client.Close();
                        _clients.Remove(client);

                        IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint;
                        string clientIP = clientEndPoint.Address.ToString();
                        int clientPort = clientEndPoint.Port;
                        //Cuối cùng, phương thức ghi log thông báo cho server và gửi thông điệp đến
                        //tất cả các client khác rằng client đó đã ngắt kết nối.
                        AppendMessage("Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                        BroadcastMessage(sender, "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                    }
                }
            }
        }
        private void RemoveClient(Socket client)
        {
            if (_clients.Contains(client)) // Kiểm tra xem client có trong danh sách _clients hay không
            {
                // Get client endpoint
                IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint; // Lấy địa chỉ IP và cổng của client
                string clientIP = clientEndPoint.Address.ToString();
                int clientPort = clientEndPoint.Port;

                // Remove client from the list
                _clients.Remove(client); // Xóa client khỏi danh sách _clients

                // Update connected clients list
                txtNickName.Invoke(new MethodInvoker(delegate
                {
                    txtNickName.Text = "";
                    foreach (Socket c in _clients)
                    {
                        IPEndPoint endPoint = c.RemoteEndPoint as IPEndPoint;
                        string ip = endPoint.Address.ToString();
                        int port = endPoint.Port;
                        txtNickName.AppendText(ip + ":" + port + "\n"); // Thêm thông tin của các client còn kết nối vào textbox txtNickName
                    }
                }));

                // Close and dispose the client socket
                client.Shutdown(SocketShutdown.Both); // Ngắt kết nối socket
                client.Close(); // Đóng socket

                // Broadcast disconnection message
                string message = "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!";
                AppendMessage(message); // Thêm thông báo ngắt kết nối vào màn hình chat
                BroadcastMessage(client, message); // Gửi thông báo ngắt kết nối cho các client khác
            }
        }

        //private void RemoveClient(Socket client)
        //{
        //    if (_clients.Contains(client))
        //    {
        //        // Get client endpoint
        //        IPEndPoint clientEndPoint = client.RemoteEndPoint as IPEndPoint;
        //        string clientIP = clientEndPoint.Address.ToString();
        //        int clientPort = clientEndPoint.Port;

        //        // Remove client from the list
        //        _clients.Remove(client);

        //        // Update connected clients list
        //        if (txtNickName.InvokeRequired)
        //        {
        //            txtNickName.Invoke(new UpdateConnectedClientsListDelegate(UpdateConnectedClientsList), client);
        //        }
        //        else
        //        {
        //            UpdateConnectedClientsList(client);
        //        }

        //        // Close and dispose the client socket
        //        client.Shutdown(SocketShutdown.Both);
        //        client.Close();

        //        // Broadcast disconnection message
        //        string message = "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!";
        //        AppendMessage(message);
        //        BroadcastMessage(client, message);
        //    }
        //}

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
                BroadcastMessage(_serverSocket, "Server ➙ " + message);
                AppendMessage("Server ➙ " + message);
                txtMessage.Clear();
            }
        }

        //Đây là phương thức ReceiveCallback được sử dụng trong
        //chương trình server để nhận dữ liệu từ các client kết nối tới.
        private void ReceiveCallback(IAsyncResult ar)
        {
            //Lấy đối tượng SocketState từ tham số AsyncState của phương thức BeginReceive.
            SocketState state = (SocketState)ar.AsyncState;
            //Lấy đối tượng Socket từ đối tượng SocketState trên.
            Socket clientSocket = state.ClientSocket;
            //Khởi tạo biến received để lưu số lượng byte dữ liệu nhận được.
            int received = 0;
            //Sử dụng phương thức EndReceive để kết thúc quá trình nhận dữ liệu và
            //lấy số lượng byte dữ liệu đã nhận được.
            //Nếu có lỗi xảy ra, sẽ gọi hàm RemoveClient để xử lý.
            try
            {
                received = clientSocket.EndReceive(ar);
            }
            catch (SocketException)
            {
                RemoveClient(clientSocket);
                return;
            }
            //Tạo một mảng byte để chứa dữ liệu nhận được và sao chép dữ liệu từ buffer của SocketState
            //vào mảng byte này.
            byte[] dataBuffer = new byte[received];
            Array.Copy(state.Buffer, 0, dataBuffer, 0, received);

            // Chuyển đổi mảng byte sang chuỗi ký tự sử dụng phương thức GetString của lớp Encoding
            string text = Encoding.UTF8.GetString(dataBuffer);

            //Lấy đối tượng IPEndPoint của client kết nối tới để lấy thông tin về IP và port của client.
            IPEndPoint clientEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
            string clientIP = clientEndPoint.Address.ToString();
            int clientPort = clientEndPoint.Port;
            //Gọi phương thức AppendMessage để hiển thị dữ liệu nhận được lên màn hình console của server.
            AppendMessage(text);
            //Nếu dữ liệu nhận được là "exit", sẽ thực hiện đóng kết nối với client, xóa client khỏi danh sách và
            //hông báo cho các client khác biết rằng một client đã ngắt kết nối. Sau đó sẽ kết thúc phương thức.
            if (text.ToLower() == "exit")
            {
                RemoveClient(clientSocket);
                AppendMessage("Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                BroadcastMessage(clientSocket, "Client " + clientIP + ":" + clientPort + " đã ngắt kết nối!");
                return;
            }
            //Nếu không, sẽ gửi dữ liệu nhận được tới các client khác bằng phương thức BroadcastMessage.
            BroadcastMessage(clientSocket, text);
            //Tiếp tục nhận dữ liệu từ client bằng phương thức BeginReceive của Socket
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
