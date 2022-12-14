using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ChessBoard chessBoard = new ChessBoard();
            chessBoard.LoadGame();
            foreach(var col in chessBoard.Cells)
            {
                foreach(var cell in col)
                {
                    this.Controls.Add(cell.BoardPic);
                    this.Controls.Add(cell.ValidMove.ValidMovePicBox);
                    if(cell.ChessPiece != null)
                    {
                        this.Controls.Add(cell.ChessPiece.ChessPicture);
                        cell.ChessPiece.ChessPicture.BringToFront();
                    }
                }
            }
        }
    }
}
