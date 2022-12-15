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
    public partial class Game : Form
    {
        ChessBoard board;
        Side moveSide;
        public Game()
        {
            InitializeComponent();
            //initialise the game
            this.board = new ChessBoard();
            this.board.LoadGame();
            this.AddAllToControl();
            this.RandomStart();
            this.UpdateTurnLabel();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            
        }
        private void RandomStart()
        {
            Random playerStart = new Random();
            //create random number between 0 and 10
            int whoStart = playerStart.Next(10);
            //red starts if smaller than 5, black starts if greater
            if (whoStart < 5)
            {
                this.moveSide = Side.Red;
            }
            else
            {
                this.moveSide = Side.Black;
            }

        }
        private void UpdateTurnLabel(Side? side = null)
        {
            if(side != null)
            {
                if (side == Side.Red)
                {
                    TurnLabel.ForeColor = Color.Red;
                }
                else
                {
                    TurnLabel.ForeColor = Color.Black;
                }
                TurnLabel.Text = $"{side} Side WIns!!!";
                return;
            }
            if(this.moveSide == Side.Red)
            {
                TurnLabel.ForeColor = Color.Red;
            }
            else
            {
                TurnLabel.ForeColor = Color.Black;
            }
            TurnLabel.Text = $"{this.moveSide} Turn";

        }
        private void ChangeSide()
        {
            if (this.moveSide == Side.Red)
            {
                this.moveSide = Side.Black;
            }
            else
            {
                this.moveSide = Side.Red;
            }
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
        private void SortCellImageOrder(Cell cell)
        {
            if (cell.ChessPiece != null)
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
        private void AddChessPieceInCellToControl(Cell cell)
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
            string cellType = typeof(Cell).ToString();
            string chessType = typeof(ChessPiece).ToString();
            string vmType = typeof(ValidMove).ToString();
            if (type == cellType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ChessBoard_Click(sender, e));
            }
            else if (type == chessType)
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
            int[] xy = boxNum.Select(num => int.Parse(num.ToString())).ToArray();

            int x = xy[0];
            int y = xy[1];
            if (this.board.FindSpecificCell(x, y, out var cell))
            {
                if (this.board.FindSelectedCell(out var selectedCell))
                {
                    this.Controls.Remove(selectedCell.ChessPiece.ChessPicture);
                    this.board.MoveChessPiece(selectedCell, cell);
                    this.Controls.Add(cell.ChessPiece.ChessPicture);
                    AddedEventHandlerToObjs(cell.ChessPiece.ChessPicture, cell.ChessPiece);
                    this.board.ClearAllSelection();
                    this.board.ClearAllValidMove();
                    this.SortCellImageOrder(cell);
                    if (this.board.CheckWinner(out Side winner))
                    {
                        MessageBox.Show($"{winner} Side Wins");
                        this.board.DisableAllPieces();
                        this.UpdateTurnLabel(winner);
                    }
                    else
                    {
                        this.ChangeSide();
                        this.board.EnableMoveAblePieces(this.moveSide);
                        this.UpdateTurnLabel();
                    }
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
            if (currentCell.ChessPiece.Side == this.moveSide)
            {
                currentCell.ChessPiece.IsSelected = true;
                var validMoves = currentCell.ChessPiece.FindValidMove(this.board);
                this.board.ShowValidMoves(validMoves);
            }
        }
        //quit game
        private void QuitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {

        }
    }
}
