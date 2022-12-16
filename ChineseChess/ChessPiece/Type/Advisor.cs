using System.Collections.Generic;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Advisor)]
    public class Advisor : ChessPiece
    {
        public Advisor(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }

        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();

            if (chessBoard.FindSpecificCell(this.X - 1, this.Y - 1, out var cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y - 1, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y + 1, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y + 1, out cell))
            {
                if (cell.AdvisorArea)
                {
                    availableCells.Add(cell);
                }
            }
            return FliterCellsToValidPoints(availableCells);
        }
    }
}
