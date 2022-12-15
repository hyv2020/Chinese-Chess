using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Cannon)]
    public class Cannonv2: ChessPiece
    {
        public Cannonv2(int x, int y, Side side, ChessBoard chessBoard): base(x, y, side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();
            //x axis moves
            //scan the whole axis
            bool firstOccupied = false;
            for(int i = this.X + 1; i < GlobalPosition.BoardSizeX; i++)
            {
                if(chessBoard.FindSpecificCell(i, this.Y, out var cell))
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
                        if(cell.ChessPiece != null)
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
            for (int i = this.Y + 1; i < GlobalPosition.BoardSizeY; i++)
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
