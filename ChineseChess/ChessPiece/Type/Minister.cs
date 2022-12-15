using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Minister)]
    public class Minister: ChessPiece
    {
        public Minister(int x, int y, Side side, ChessBoard chessBoard): base(x, y, side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            if (CheckOccupiedCell(1, 1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X + 2, this.Y + 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (CheckOccupiedCell(-1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X - 2, this.Y - 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (CheckOccupiedCell(1, -1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X + 2, this.Y - 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (CheckOccupiedCell(-1, 1, chessBoard))
            {
                if (chessBoard.FindSpecificCell(this.X - 2, this.Y + 2, out var cell))
                {
                    if (CheckCrossRiver(cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            return FliterCellsToValidPoints(availableCells);
        }
        private bool CheckOccupiedCell(int xOffset, int yOffset, ChessBoard chessBoard)
        {
            if (chessBoard.FindSpecificCell(this.X + xOffset, this.Y + yOffset, out var occupiedCell))
            {
                return occupiedCell.ChessPiece == null;
            }
            return false;
        }
        private bool CheckCrossRiver(Cell destCell)
        {
            return destCell.Side == this.Side;
        }
    }
}
