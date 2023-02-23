using System.Collections.Generic;
using GameCommons;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Cannon)]
    public class Cannon : ChessPiece
    {
        public Cannon(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
            //x axis moves
            //scan the whole axis
            bool firstOccupied = false;
            for (int i = this.X + 1; i < GlobalVariables.BoardSizeX; i++)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        availableCells.Add(cell);
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            availableCells.Add(cell);
                            break;
                        }
                    }
                }
            }
            firstOccupied = false;
            for (int i = this.X - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        availableCells.Add(cell);
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            availableCells.Add(cell);
                            break;
                        }
                    }
                }
            }
            firstOccupied = false;
            for (int i = this.Y + 1; i < GlobalVariables.BoardSizeY; i++)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        availableCells.Add(cell);
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            availableCells.Add(cell);
                            break;
                        }
                    }
                }
            }
            firstOccupied = false;
            for (int i = this.Y - 1; i >= 0; i--)
            {
                if (chessBoard.FindSpecificCell(this.X, i, out var cell))
                {
                    if (!firstOccupied)
                    {
                        if (cell.ChessPiece != null)
                        {
                            firstOccupied = true;
                            continue;
                        }
                        availableCells.Add(cell);
                    }
                    else
                    {
                        if (cell.ChessPiece != null)
                        {
                            availableCells.Add(cell);
                            break;
                        }
                    }
                }
            }
            return FliterCellsToValidPoints(availableCells);
        }
    }
}
