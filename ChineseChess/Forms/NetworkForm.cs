using ChineseChess.Forms;
using GameClient;
using GameCommons;
using GameServer;
using NetworkCommons;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class NetworkForm : Form
    {
        AsynchronousClient client;
        public NetworkForm()
        {
            InitializeComponent();
        }

        private void HostGameButton_Click(object sender, EventArgs e)
        {
            NetworkGame game = new NetworkGame();
            game.Show();
            this.Visible = false;
            this.Close();
            this.Dispose();
            //listener= new AsynchronousTCPListener();
            //var listen = listener.StartListeningAsync();
            //await listen;
        }

        private async void JoinGameButton_Click(object sender, EventArgs e)
        {
            if (ServerIPTextBox.Text.Count() > 0 && IPAddress.TryParse(ServerIPTextBox.Text, out var iPAddress))
            {
                NetworkGame game = new NetworkGame(ServerIPTextBox.Text);
                game.Show();
                this.Visible = false;
                this.Close();
                this.Dispose();
            }
            else
            {
                MessageBox.Show("Invalid IP", "Invalidate IP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*
            client = new AsynchronousClient("192.168.1.0");
            try
            {
                await client.ConnectAsync();

            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine("ArgumentNullException: {0}", ex);
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("SocketException: {0}", ex);
            }
            */
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            client = null;
            this.Close();
            this.Dispose();
        }
        private void NetworkForm_Load(object sender, EventArgs e)
        {

        }
    }
}
