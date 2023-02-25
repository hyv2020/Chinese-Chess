using GameClient;
using GameServer;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using GameCommons;
using System.ComponentModel;
using System.IO;
using System.Text;
using NetworkCommons;

namespace ChineseChess
{
    public partial class NetworkForm : Form, IClientObserver
    {
        AsynchronousTCPListener listener;
        AsynchronousClient client;
        public NetworkForm()
        {
            InitializeComponent();
        }

        private async void HostGameButton_Click(object sender, EventArgs e)
        {
            listener= new AsynchronousTCPListener();
            var listen = listener.StartListeningAsync();
            await listen;
        }

        private async void JoinGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                // ip address of current dervice
                string localIP = NetworkCommons.IP.GetCurrentMachineIP();
                client = new AsynchronousClient(ServerIPTextBox.Text);
                client.RegisterObserver(this);
                var connectServer = client.ConnectAsync(ServerIPTextBox.Text);
                await connectServer;
            }
            catch
            {
                Debug.WriteLine($"{ServerIPTextBox.Text} connection failed");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            listener.StopListening();
            client.UnregisterObserver(this);
            client.Disconnect();
            client = null;
            this.Close();
            this.Dispose();
        }
        private void NetworkForm_Load(object sender, EventArgs e)
        {
            
        }
        public void OnTcpDataReceived(object data)
        {
            if(data as Turn != null)
            {

            }
            Debug.WriteLine($"Observer Received data: {data}");
        }
        private async void ClientSendDataButton_Click(object sender, EventArgs e)
        {
            var sendData = client.SendMessageAsync("test");
            await sendData;
        }
    }
}
