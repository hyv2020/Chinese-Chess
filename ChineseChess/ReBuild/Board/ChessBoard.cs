using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class ChessBoard
    {
        public List<List<Cellv2>> Cells { get; set; }
        private static List<string> defaultStart = new List<string>()
        {
            "15 11 12 13 16 13 12 11 15",
            "0 0 0 0 0 0 0 0 0",
            "0 14 0 0 0 0 0 14 0",
            "10 0 10 0 10 0 10 0 10",
            "0 0 0 0 0 0 0 0 0",
            "0 0 0 0 0 0 0 0 0",
            "00 0 00 0 00 0 00 0 00",
            "0 04 0 0 0 0 0 04 0",
            "0 0 0 0 0 0 0 0 0",
            "05 01 02 03 06 03 02 01 05",
        };
        public ChessBoard()
        {
            this.Cells = new List<List<Cellv2>>();
            for(int x = 0; x < GlobalPosition.BoardSizeX; x++)
            {
                List<Cellv2> column = new List<Cellv2>();
                for(var y = 0; y < GlobalPosition.BoardSizeY; y++)
                {
                    column.Add(new Cellv2(x, y));
                }
                this.Cells.Add(column);
            }
        }
        public void LoadGame(List<string> matchData = null)
        {
            matchData = matchData?? defaultStart;
            List<List<string>> board = new List<List<string>>();
            foreach(var row in matchData)
            {
                List<string> rowData = row.Split(' ').ToList();
                board.Add(rowData);
            }
            for(int y = 0; y < board.Count; y++)
            {
                for(int x = 0; x < board[y].Count; x++)
                {
                    char[] cell = board[y][x].ToCharArray();
                    if (cell.Length > 1)
                    {
                        Side side = (Side)Enum.Parse(typeof(Side), cell.First().ToString());
                        ChessPieceType chessPieceType = (ChessPieceType)Enum.Parse(typeof(ChessPieceType), cell.Last().ToString());

                        this.Cells[x][y].AddChessPiece(side, chessPieceType, this);
                    }
                    else
                    {
                        this.Cells[x][y].RemoveChessPiece();
                    }

                }
            }
        }

    }
}
