using ChineseChess.Forms;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class NetworkForm : Form
    {
        public NetworkForm()
        {
            InitializeComponent();
        }

        private void HostGameButton_Click(object sender, EventArgs e)
        {
            NetworkGame game = new NetworkGame();
            game.Show();
            CloseForm();
        }

        private void JoinGameButton_Click(object sender, EventArgs e)
        {
            if (ServerIPTextBox.Text.Count() > 0 && IPAddress.TryParse(ServerIPTextBox.Text, out var iPAddress))
            {
                NetworkGame game = new NetworkGame(ServerIPTextBox.Text);
                if (game.clientConnected)
                {
                    game.Show();
                    CloseForm();
                }
            }
            else
            {
                MessageBox.Show("Enter vaild IP", "Invalid IP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Program.mainMenu.Visible = true;
        }
        private void NetworkForm_Load(object sender, EventArgs e)
        {

        }
        private void CloseForm()
        {
            this.Visible = false;
            this.Close();
            this.Dispose();
        }
    }
}
