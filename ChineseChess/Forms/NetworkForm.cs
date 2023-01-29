using GameClient;
using GameServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class NetworkForm : Form
    {
        AsynchronousTCPListener listener = new AsynchronousTCPListener();
        AsynchronousClient client = new AsynchronousClient("192.168.1.96");
        public NetworkForm()
        {
            InitializeComponent();
        }

        private void HostGameButton_Click(object sender, EventArgs e)
        {
            listener.StartListeningAsync();
           
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            try
            {
                //listener.SendMessage();
                client.ConnectAsync("192.168.1.96");
            }
            catch
            {
                Debug.WriteLine("192.168.1.96 connection failed");
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
    }
}
