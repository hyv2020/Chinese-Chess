using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Soldier)]
    public class Soldierv2: ChessPiece
    {
        public Soldierv2(int x, int y, Side side, ChessBoard chessBoard): base(x,y,side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();
            if (this.Side == Side.Red)
            {
                if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out var cell))
                {
                    availableCells.Add(cell);
                }
            }
            else
            {
                if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out var cell))
                {
                    availableCells.Add(cell);
                }
            }
            availableCells.AddRange(CheckCrossRiverMoves(chessBoard));

            return FliterCellsToValidPoints(availableCells);
        }
        private List<Cellv2> CheckCrossRiverMoves(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();
            if (chessBoard.FindSpecificCell(this.X, this.Y, out var currentCell))
            {
                if (currentCell.Side != this.Side)
                {
                    if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out var cell))
                    {
                        availableCells.Add(cell);
                    }
                    if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            return availableCells;
        }

    }
}
