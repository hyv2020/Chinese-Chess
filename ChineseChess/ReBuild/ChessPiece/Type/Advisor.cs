using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Advisor)]
    public class Advisorv2: ChessPiece
    {
        public Advisorv2(int x, int y, Side side, ChessBoard chessBoard): base(x, y, side, chessBoard)
        {

        }

        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();

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
