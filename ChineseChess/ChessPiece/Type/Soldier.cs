using GameCommons;
using System.Collections.Generic;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Soldier)]
    public class Soldier : ChessPiece
    {
        public Soldier(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
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
        private List<Cell> CheckCrossRiverMoves(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
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
