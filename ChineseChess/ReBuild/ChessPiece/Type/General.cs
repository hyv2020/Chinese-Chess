using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.General)]
    public class Generalv2:ChessPiece
    {
        public Generalv2(int x, int y, Side side, ChessBoard chessBoard): base(x, y, side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if(CheckFlyingGeneral(out cell, chessBoard))
            {
                availableCells.Add(cell);
            }
            return FliterCellsToValidPoints(availableCells);
        }
        private bool CheckFlyingGeneral(out Cellv2 cell, ChessBoard chessBoard)
        {
            for (int i = 0; i < GlobalPosition.BoardSizeY; i++)
            {
                if (i != this.Y && chessBoard.FindSpecificCell(this.X, i, out cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        if (cell.ChessPiece.GetChessPieceType() == ChessPieceType.General)
                        {
                            return true;
                        }
                        return false;
                    }
                }
            }
            cell = null;
            return false;
        }
    }
}
