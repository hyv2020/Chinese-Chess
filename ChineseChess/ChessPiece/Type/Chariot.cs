using System.Collections.Generic;
using GameCommons;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Chariot)]
    public class Chariot : ChessPiece
    {
        public Chariot(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            //x axis moves
            //scan the whole axis
            for (int i = this.X + 1; i < GlobalVariables.BoardSizeX; i++)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        availableCells.Add(cell);
                        break;
                    }
                    else
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            for (int i = this.X - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        availableCells.Add(cell);
                        break;
                    }
                    else
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            for (int i = this.Y + 1; i < GlobalVariables.BoardSizeY; i++)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        availableCells.Add(cell);
                        break;
                    }
                    else
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            for (int i = this.Y - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (cell.ChessPiece != null)
                    {
                        availableCells.Add(cell);
                        break;
                    }
                    else
                    {
                        availableCells.Add(cell);
                    }
                }
            }
            return FliterCellsToValidPoints(availableCells);
        }
    }
}
