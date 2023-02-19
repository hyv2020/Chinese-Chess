using ChineseChess;
using NetworkCommons;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameClient
{
    public class AsynchronousClient
    {
        string serverIP;
        TcpClient tcpClient;

        public AsynchronousClient(string server)
        {
            this.serverIP = server;
        }
        public async Task ConnectAsync(string message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = Ports.remotePort;

                // Not a using declaration to the instance stays connected.
                tcpClient = new TcpClient(serverIP, port);
                while (true)
                {
                    try
                    {
                        if (!tcpClient.Connected)
                        {
                            tcpClient.Connect(serverIP, port);
                        }
                        // Send and receive data here...
                        await Listen();
                        //await SendMessageAsync(new Turn());
                        // Close the stream and the client when finished
                        //stream.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions here...
                        // If the connection was lost, the loop will start over
                    }
                }

            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
        }
        public async Task Nothing()
        {
            Debug.WriteLine("Client connected");
            Thread.Sleep(5000);
        }
        public async Task SendMessageAsync(object message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = Ports.remotePort;
                Turn turn = message as Turn;
                if (turn is null)
                {
                    throw new ArgumentNullException(nameof(turn));
                }

                // Prefer a using declaration to ensure the instance is Disposed later.
                // Translate the passed message and store it as a Byte array.
                byte[] data = turn.ToByteArray();

                // Get a client stream for reading and writing.
                NetworkStream stream = tcpClient.GetStream();
                // Send the message to the connected TcpServer.
                await stream.WriteAsync(data, 0, data.Length);

                Debug.WriteLine($"Sent: {turn}", this.serverIP.ToString());

                // Receive the server response.

                // Buffer to store the response bytes.
                data = new byte[Ports.bufferSize];
                // Read the first batch of the TcpServer response bytes.
                int bytes = await stream.ReadAsync(data, 0, data.Length);
                // data recieved from server
                var responseData = Turn.ByteArrayToObject(data);

                Debug.WriteLine($"Received: {responseData}", this.serverIP.ToString());

                // Explicit close is not necessary since TcpClient.Dispose() will be
                // called automatically.
                // stream.Close();
                // client.Close();

            }
            catch (ArgumentNullException e)
            {
                Debug.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }

        }
        private async Task Listen()
        {
            NetworkStream stream = tcpClient.GetStream();
            // Buffer to store the response bytes.
            var data = new byte[Ports.bufferSize];
            // Read the first batch of the TcpServer response bytes.
            int bytes = await stream.ReadAsync(data, 0, data.Length);
            // data recieved from server
            var responseData = Turn.ByteArrayToObject(data);

            Debug.WriteLine($"Received: {responseData}", this.serverIP.ToString());
        }
        public void Disconnect()
        {
            tcpClient = null;
        }
    }
}
