using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess
{
    public abstract class ChessPiece
    {
        public readonly string Name;
        public readonly int X;
        public readonly int Y;
        public readonly Side Side;
        public PictureBox ChessPicture { get; set; }
        public bool CanMove { get; set; }
        public ChessPiece(int x, int y, Side side, ChessBoard chessBoard)
        {
            ChessPieceType chessPieceType = this.GetChessPieceType();
            var count = chessBoard.Cells.SelectMany(col => col)
                .Where(cell => cell.ChessPiece.GetChessPieceType() == chessPieceType).Count() + 1;
            this.Name = $"{side}{chessPieceType}{count}";
            this.X = x;
            this.Y = y;
            this.Side = side;
            this.ChessPicture = DrawChessPieceFunctions.DrawChessPiece(this);
            this.CanMove = false;
        }
        
        public void RemoveChessPiecePic()
        {
            this.ChessPicture.Dispose();
        }
        public abstract List<Point> FindValidMove(ChessBoard chessBoard);
    }
}
