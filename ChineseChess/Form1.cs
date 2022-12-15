using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
namespace ChineseChess
{
    public partial class Form1 : Form
    {
        ChessBoard board;
        public Form1()
        {
            InitializeComponent();
            this.board = new ChessBoard();
            this.board.LoadGame();
            this.AddAllToControl();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void AddAllToControl()
        {
            foreach (var col in this.board.Cells)
            {
                foreach (var cell in col)
                {
                    this.Controls.Add(cell.BoardPic);
                    AddedEventHandlerToObjs(cell.BoardPic, cell);
                    this.Controls.Add(cell.ValidMove.ValidMovePicBox);
                    AddedEventHandlerToObjs(cell.ValidMove.ValidMovePicBox, cell.ValidMove);
                    this.AddChessPieceInCellToControl(cell);
                    SortCellImageOrder(cell);
                }
            }
        }
        private void SortCellImageOrder(Cellv2 cell)
        {
            if(cell.ChessPiece != null)
            {
                cell.ChessPiece.ChessPicture.BringToFront();
            }
            cell.ValidMove.ValidMovePicBox.BringToFront();
        }
        private void AddCellsToControl()
        {
            foreach (var col in this.board.Cells)
            {
                foreach (var cell in col)
                {
                    this.Controls.Add(cell.BoardPic);
                    AddedEventHandlerToObjs(cell.BoardPic, cell);
                    this.Controls.Add(cell.ValidMove.ValidMovePicBox);
                    AddedEventHandlerToObjs(cell.ValidMove.ValidMovePicBox, cell.ValidMove);
                    SortCellImageOrder(cell);
                }
            }
        }
        private void AddChessPieceInCellToControl(Cellv2 cell)
        {
            if (cell.ChessPiece != null)
            {
                this.Controls.Add(cell.ChessPiece.ChessPicture);
                AddedEventHandlerToObjs(cell.ChessPiece.ChessPicture, cell.ChessPiece);
            }
        }
        

        private void AddedEventHandlerToObjs<T>(PictureBox pictureBox, T obj)
        {
            var type = obj.ToString();
            string cellType = typeof(Cellv2).ToString();
            string chessType = typeof(ChessPiece).ToString();
            string vmType = typeof(ValidMove).ToString();
            if(type == cellType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ChessBoard_Click(sender, e));
            }
            else if(type == chessType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ChessPiece_Click(sender, e));
            }
            else if (type == vmType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ValidMove_Click(sender, e));
            }
        }

        private void ChessBoard_Click(object sender, EventArgs e)
        {
            this.board.ClearAllValidMove();
        }
        private void ValidMove_Click(object sender, EventArgs e)
        {
            var pic = (PictureBox)sender;
            var boxName = pic.Name;
            var boxNum = Regex.Match(boxName, @"\d+").Value;
            int[] xy = boxNum.Select(num=>int.Parse(num.ToString())).ToArray();

            int x = xy[0];
            int y = xy[1];
            if (this.board.FindSpecificCell(x, y, out var cell))
            {
                if(this.board.FindSelectedCell(out var selectedCell))
                {
                    this.Controls.Remove(selectedCell.ChessPiece.ChessPicture);
                    this.board.MoveChessPiece(selectedCell, cell);
                    this.Controls.Add(cell.ChessPiece.ChessPicture);
                    AddedEventHandlerToObjs(cell.ChessPiece.ChessPicture, cell.ChessPiece);
                    this.board.ClearAllSelection();
                    this.board.ClearAllValidMove();
                    this.SortCellImageOrder(cell);
                    //this.board.CheckWinner(out Side winner);
                }
            }
        }
        private void ChessPiece_Click(object sender, EventArgs e)
        {
            this.board.ClearAllSelection();
            this.board.ClearAllValidMove();
            var allCells = this.board.GetAllCellsInOneList();
            var cellWithChessPiece = allCells.Where(x => x.ChessPiece != null);
            var currentCell = cellWithChessPiece.Single(x => x.ChessPiece.ChessPicture == (PictureBox)sender);
            currentCell.ChessPiece.IsSelected = true;
            var validMoves = currentCell.ChessPiece.FindValidMove(this.board);
            this.board.ShowValidMoves(validMoves);
        }
    }
}
