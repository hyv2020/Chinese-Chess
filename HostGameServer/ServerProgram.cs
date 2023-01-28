using NetworkCommons;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace GameServer
{
    public class AsynchronousTCPListener
    {

        public AsynchronousTCPListener()
        {

        }
        TcpListener server = new TcpListener(IPAddress.Any, Ports.remotePort);

        public void StartListening()
        {
            ServerStart();  //starting the server

            Debug.WriteLine("Server Started");
        }

        public void StopListening()
        {
            server.Stop();
        }

        private void ServerStart()
        {
            server.Start();
            AcceptConnection();  //accepts incoming connections
        }
         
        private void AcceptConnection()
        {
            try
            {
                server.BeginAcceptTcpClient(HandleConnection, server);  //this is called asynchronously and will run in a different thread
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Server closed");
            }
        }

        private void HandleConnection(IAsyncResult result)  //the parameter is a delegate, used to communicate between threads
        {
            try
            {
                AcceptConnection();  //once again, checking for any other incoming connections
                TcpClient client = server.EndAcceptTcpClient(result);  //creates the TcpClient
                using (client)
                {
                    NetworkStream ns = client.GetStream();

                    /* here you can add the code to send/receive data */
                    byte[] hello = new byte[100];   //any message must be serialized (converted to byte array)
                    hello = Encoding.Default.GetBytes(IP.GetCurrentMachineIP());  //conversion string => byte array

                    ns.Write(hello, 0, hello.Length);     //sending the message

                    while (client.Connected)  //while the client is connected, we look for incoming messages
                    {
                        byte[] msg = new byte[1024];     //the messages arrive as byte array
                        ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
                        Debug.WriteLine(Encoding.Default.GetString(msg)); //now , we write the message as string
                    }
                }
                client.Close();
                client.Dispose();
            }
            catch
            {
                return;
            }        
        }
    }

}
