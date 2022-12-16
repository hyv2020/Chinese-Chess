using System.Collections.Generic;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Horse)]
    public class Horse : ChessPiece
    {

        public Horse(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            if (chessBoard.FindSpecificCell(this.X - 1, this.Y, out var occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X - 2, this.Y - 1, out var cell))
                    {
                        availableCells.Add(cell);
                    }
                    if (chessBoard.FindSpecificCell(this.X - 2, this.Y + 1, out cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (chessBoard.FindSpecificCell(this.X + 1, this.Y, out occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 2, this.Y - 1, out var cell))
                    {
                        availableCells.Add(cell);
                    }
                    if (chessBoard.FindSpecificCell(this.X + 2, this.Y + 1, out cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (chessBoard.FindSpecificCell(this.X, this.Y + 1, out occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 1, this.Y + 2, out var cell))
                    {
                        availableCells.Add(cell);
                    }
                    if (chessBoard.FindSpecificCell(this.X - 1, this.Y + 2, out cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            if (chessBoard.FindSpecificCell(this.X, this.Y - 1, out occupiedCell))
            {
                if (occupiedCell.ChessPiece == null)
                {
                    if (chessBoard.FindSpecificCell(this.X + 1, this.Y - 2, out var cell))
                    {
                        availableCells.Add(cell);
                    }
                    if (chessBoard.FindSpecificCell(this.X - 1, this.Y - 2, out cell))
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            return FliterCellsToValidPoints(availableCells);
        }
    }
}
