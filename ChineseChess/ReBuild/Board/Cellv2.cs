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
        int x;
        int y;
        public int X { get { return this.x; } }
        public int Y { get { return this.y; } }
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
        private bool advisorArea =false;
        public bool AdvisorArea
        {
            get { return this.advisorArea; }
        }
        public readonly ValidMove ValidMove;
        public PictureBox BoardPic;

        public Cellv2(int x, int y)
        {
            this.x = x; 
            this.y = y;
            chessPiece = null;
            BoardPic = DrawBoardFunctions.DrawBoard(x, y);
            side = (y > 5) ? Side.Red : Side.Black;
            ValidMove = new ValidMove(x, y);
            if ((x < 6 && x > 2) && (y < 3 || y > GlobalPosition.BoardSizeY - 3))
            {
                this.advisorArea = true;
            }
        }
        public void AddChessPiece(Side side, ChessPieceType chessPieceType, ChessBoard chessBoard)
        {
            this.chessPiece = ChessPieceFactory.CreateChessPiece(this.X, this.Y, side, chessPieceType, chessBoard);
        }
        public void MoveChessPiece(ChessPiece chessPiece, ChessBoard chessBoard)
        {
            if(this.chessPiece != null)
            {
                this.RemoveChessPiece();
            }
            this.chessPiece = ChessPieceFactory.CloneChessPieceToNewLocation(this.X, this.Y, chessPiece, chessBoard);
        }
        public void RemoveChessPiece()
        {
            if (this.chessPiece != null)
            {
                this.chessPiece.RemoveChessPiecePic();
            }
            this.chessPiece = null;
        }
        public override string ToString()
        {
            return typeof(Cellv2).ToString();
        }
    }
}
