using System;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Game newGame = new Game();
            newGame.Show();
            this.Visible = false;

        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void RulesButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ymimports.com/pages/how-to-play-xiangqi-chinese-chess");
        }
    }
}
