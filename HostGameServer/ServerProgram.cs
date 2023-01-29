using NetworkCommons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameServer
{
    public class AsynchronousTCPListener
    {

        public AsynchronousTCPListener()
        {

        }
        TcpListener server = new TcpListener(IPAddress.Any, Ports.remotePort);

        static List<TcpClient> clients = new List<TcpClient>();
        public async Task StartListeningAsync()
        {
            try
            {
                // Start listening for client requests.
                server.Start();

                // Enter the listening loop.
                while (true)
                {
                    Debug.WriteLine("Waiting for a connection... ");

                    // max clients connection
                    if(clients.Count < 3) 
                    {
                        // Perform a blocking call to accept requests.
                        // You could also use server.AcceptSocket() here.
                        TcpClient client = await server.AcceptTcpClientAsync();

                        // Add the new client to the list of clients
                        clients.Add(client);

                        Debug.WriteLine("Connected!");

                        // Start a new thread to handle communication
                        // with connected client
                        await HandleClientAsync(client);
                    }
                    
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                //server.Stop();
            }

            Debug.WriteLine("\nHit enter to continue...");
        }
        public void StopListening() => server.Stop();
        private async Task HandleClientAsync(object obj)
        {
            // Retrieve the client from the parameter passed to the thread
            TcpClient client = (TcpClient)obj;
            using (client)
            {
                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();
                using (stream)
                {
                    Byte[] data = new Byte[256];

                    
                    data = Encoding.Default.GetBytes("Server");
                    int bytes = data.Length;
                    string clientData = Encoding.Default.GetString(data, 0, bytes);

                    // recieve data from client
                    //bytes = await stream.ReadAsync(data, 0, data.Length);
                    //clientData = Encoding.Default.GetString(data, 0, bytes);

                    Debug.WriteLine("Received: {0}", clientData);

                    // Loop through the list of clients and send the message to all clients except the sender
                    foreach (TcpClient otherClient in clients)
                    {
                        if (!otherClient.Equals(client))
                        {
                            // send data to different client
                            NetworkStream otherStream = otherClient.GetStream();
                            Debug.WriteLine("Redirect: {0}", clientData);
                            await otherStream.WriteAsync(data, 0, bytes);
                        }
                        else
                        {
                            // return message back to same client
                            Debug.WriteLine("Return: {0}", clientData);
                            await stream.WriteAsync(data, 0, bytes);
                        }
                    }
                }
            } 
        }
    }

}
