using System.Collections.Generic;
using GameCommons;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.General)]
    public class General : ChessPiece
    {
        public General(int x, int y, Side side, ChessBoard chessBoard) : base(x, y, side, chessBoard)
        {

        }
        public override IEnumerable<Cell> FindValidMove(ChessBoard chessBoard)
        {
            List<Cell> availableCells = new List<Cell>();
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
            if (CheckFlyingGeneral(out cell, chessBoard))
            {
                availableCells.Add(cell);
            }
            return FliterCellsToValidPoints(availableCells);
        }
        private bool CheckFlyingGeneral(out Cell cell, ChessBoard chessBoard)
        {
            if (this.Side == Side.Red)
            {
                for (int i = this.Y; i >= 0; i--)
                {
                    if (i != this.Y && chessBoard.FindSpecificCell(this.X, i, out cell))
                    {
                        if (cell.ChessPiece != null)
                        {
                            if (cell.ChessPiece.GetChessPieceType() == ChessPieceType.General)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        continue;
                    }
                }
            }
            else
            {
                for (int i = this.Y; i < GlobalVariables.BoardSizeY; i++)
                {
                    if (i != this.Y && chessBoard.FindSpecificCell(this.X, i, out cell))
                    {
                        if (cell.ChessPiece != null)
                        {
                            if (cell.ChessPiece.GetChessPieceType() == ChessPieceType.General)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        continue;
                    }
                }
            }

            cell = null;
            return false;
        }

    }
}
