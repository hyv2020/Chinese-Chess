using GameCommons;
using NetworkCommons;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{
    public class AsynchronousTCPListener
    {

        public AsynchronousTCPListener()
        {

        }
        TcpListener server = new TcpListener(IPAddress.Any, Ports.remotePort);

        static HashSet<TcpClient> clients = new HashSet<TcpClient>();
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

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = await server.AcceptTcpClientAsync();
                    // Add the new client to the list of clients
                    clients.Add(client);
                    Debug.WriteLine("Client Connected!");
                    // Start a new thread to handle communication
                    // with connected client
                    while (client.Connected) 
                    {
                        var message = await ListenClientAsync(client);
                        await RedirectToClientAsync(message, client);
                        //await HandleClientAsync(client);
                        Debug.WriteLine("Continue listening... ");
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
        
        private async Task<Turn> ListenClientAsync(object obj)
        {
            // Retrieve the client from the parameter passed to the thread
            TcpClient client = (TcpClient)obj;

            // Get a stream object for reading and writing
            NetworkStream stream = client.GetStream();
            byte[] data = new byte[Ports.bufferSize];
            //data = Encoding.Default.GetBytes("Server");
            // recieve data from client
            int bytes = await stream.ReadAsync(data, 0, data.Length);
            var clientData = Turn.ByteArrayToObject(data);
            Debug.WriteLine($"Received: {clientData}", "Server");
            return clientData;
        }

        private async Task RedirectToClientAsync(Turn obj, TcpClient client)
        {
            var data = obj.ToByteArray();
            try
            {
                foreach (TcpClient otherClient in clients)
                {
                    NetworkStream stream = client.GetStream();
                    if (!otherClient.Equals(client))
                    {
                        // send data to different client
                        NetworkStream otherStream = otherClient.GetStream();
                        Debug.WriteLine($"Redirect: {obj}", "Server");
                        await otherStream.WriteAsync(data, 0, data.Length);
                    }
                    else
                    {
                        // return message back to same client
                        Debug.WriteLine($"Return: {obj}", "Server");
                        await stream.WriteAsync(data, 0, data.Length);
                    }

                }
            }
            catch
            {
                Debug.WriteLine("Server Return/Redirect message failed");
            }

        }

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
                    byte[] data = new byte[Ports.bufferSize];
                    //data = Encoding.Default.GetBytes("Server");
                    // recieve data from client
                    int bytes = await stream.ReadAsync(data, 0, data.Length);
                    var clientData = Turn.ByteArrayToObject(data);
                    Debug.WriteLine($"Received: {clientData}", "Server");

                    // Loop through the list of clients and send the message to all clients except the sender
                    try
                    {
                        foreach (TcpClient otherClient in clients)
                        {
                            if (!otherClient.Equals(client))
                            {
                                // send data to different client
                                NetworkStream otherStream = otherClient.GetStream();
                                Debug.WriteLine($"Redirect: {clientData}", "Server");
                                await otherStream.WriteAsync(data, 0, data.Length);
                            }
                            else
                            {
                                // return message back to same client
                                Debug.WriteLine($"Return: {clientData}", "Server");
                                await stream.WriteAsync(data, 0, data.Length);
                            }
                        }
                    }
                    catch
                    {
                        Debug.WriteLine("Server Return/Redirect message failed");
                    }
                }
            }
        }

    }

}
