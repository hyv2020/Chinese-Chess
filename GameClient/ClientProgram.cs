﻿using NetworkCommons;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task ConnectAsync(String message)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = Ports.remotePort;

                // Prefer a using declaration to ensure the instance is Disposed later.
                tcpClient = new TcpClient(serverIP, port);
                using (tcpClient)
                {
                    // Translate the passed message into ASCII and store it as a Byte array.
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                    // Get a client stream for reading and writing.
                    NetworkStream stream = tcpClient.GetStream();
                    using(stream)
                    {
                        // Send the message to the connected TcpServer.
                        await stream.WriteAsync(data, 0, data.Length);

                        Debug.WriteLine("Sent: {0}", message);

                        // Receive the server response.

                        // Buffer to store the response bytes.
                        data = new Byte[256];

                        // String to store the response ASCII representation.
                        String responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = await stream.ReadAsync(data, 0, data.Length);
                        // data recieved from server
                        responseData = Encoding.Default.GetString(data, 0, bytes);

                        Debug.WriteLine("Received: {0}", message);

                        // Explicit close is not necessary since TcpClient.Dispose() will be
                        // called automatically.
                        // stream.Close();
                        // client.Close();
                    }

                };
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
        public void Disconnect()
        {
            tcpClient= null;
        }
    }
}
