using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Chariot)]
    public class Chariotv2: ChessPiece
    {
        public Chariotv2(int x, int y, Side side, ChessBoard chessBoard): base(x, y, side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            List<Cellv2> availableCells = new List<Cellv2>();
            //x axis moves
            //scan the whole axis
            for (int i = this.X + 1; i < GlobalPosition.BoardSizeX; i++)
            {
                if (chessBoard.FindSpecificCell(i, this.Y, out var cell))
                {
                    if(cell.ChessPiece != null)
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
            for (int i = this.Y + 1; i < GlobalPosition.BoardSizeY; i++)
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
