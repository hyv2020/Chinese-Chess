using GameClient;
using GameServer;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class NetworkForm : Form
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
            client.Disconnect();
            client = null;
            this.Close();
            this.Dispose();
        }

        private void NetworkForm_Load(object sender, EventArgs e)
        {

        }

        private async void ClientSendDataButton_Click(object sender, EventArgs e)
        {
            var sendData = client.SendMessageAsync(new Turn());
            await sendData;
        }
    }
}
