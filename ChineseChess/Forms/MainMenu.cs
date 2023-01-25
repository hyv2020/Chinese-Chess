﻿using System;
using System.Windows.Forms;
using GameServer;
using GameClient;

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
            UtilOps.ClearTempFolder();
            System.Windows.Forms.Application.Exit();
        }

        private void RulesButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.ymimports.com/pages/how-to-play-xiangqi-chinese-chess");
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FilePaths.rootSaveFilePath;
            openFileDialog.Title = GlobalVariables.LoadDialogTitle;
            openFileDialog.Filter = GlobalVariables.LoadDialogFliter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string saveFileName = openFileDialog.FileName;
                Game newGame = new Game(saveFileName);
                newGame.Show();
                this.Visible = false;
            }
        }

        private void networkModeButton_Click(object sender, EventArgs e)
        {
            AsynchronousSocketListener.StartListening();
            AsynchronousClient.StartClient();

        }
    }
}
