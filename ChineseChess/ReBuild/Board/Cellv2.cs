using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class Cellv2
    {
        int X { get; set; }
        int Y { get; set; }
        private ChessPiece chessPiece;
        public ChessPiece ChessPiece 
        { 
            get { return chessPiece; } 
        }
        private Side side;
        public Side Side
        {
            get { return this.side; }
        }
        public bool IsSelected { get; set; }

        private bool advisorArea = false;
        public bool AdvisorArea
        {
            get { return this.advisorArea; }
        }
        public ValidMove ValidMove;
        public PictureBox BoardPic;

        public Cellv2(int x, int y)
        {
            this.X = x; 
            this.Y = y;
            this.chessPiece = null;
            this.BoardPic = DrawBoardFunctions.DrawBoard(x, y);
            this.side = (y > 5) ? Side.Red : Side.Black;
            this.ValidMove = new ValidMove(x, y);
            if ((x < 6 && x > 2) && (y > 3 || y > GlobalPosition.BoardSizeY - 2))
            {
                this.advisorArea = true;
            }
            this.IsSelected = false;
        }
        public void AddChessPiece(Side side, ChessPieceType chessPieceType, ChessBoard chessBoard)
        {
            this.chessPiece = ChessPieceFactory.CreateChessPiece(this.X, this.Y, side, chessPieceType, chessBoard);
        }
        public void RemoveChessPiece()
        {
            if (this.chessPiece != null)
            {
                this.chessPiece.RemoveChessPiecePic();
            }
            this.chessPiece = null;
        }
    }
}
