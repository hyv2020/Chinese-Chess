using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChineseChess
{
    public class ChessBoard
    {
        public List<List<Cell>> Cells { get; set; }
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
            this.Cells = new List<List<Cell>>();
            for(int x = 0; x < GlobalVariables.BoardSizeX; x++)
            {
                List<Cell> column = new List<Cell>();
                for(var y = 0; y < GlobalVariables.BoardSizeY; y++)
                {
                    column.Add(new Cell(x, y));
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
        public IEnumerable<string> SaveGame()
        {
            List<string> saveData = new List<string>();
            var allCellsInGroupsOfX = this.GetAllCellsInOneList().GroupBy(c=>c.Y);
            foreach(var group in allCellsInGroupsOfX)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var cell in group)
                {
                    if(cell.ChessPiece is null)
                    {
                        sb.Append("0 ");
                    }
                    else
                    {
                        sb.Append($"{(int)cell.ChessPiece.Side}{(int)cell.ChessPiece.GetChessPieceType()} ");
                    }
                }
                sb.Remove(sb.Length - 1, 1);
                yield return sb.ToString();
            }

        }
        public IEnumerable<Cell> GetAllCellsInOneList()
        {
            return this.Cells.SelectMany(x => x);
        }
        private IEnumerable<Cell> GetAllChessPieces()
        {
            return this.GetAllCellsInOneList().Where(y => y.ChessPiece != null);
        }
        public void ClearBoard()
        {
            var allChessPiece = this.GetAllChessPieces();
            foreach(var chessPiece in allChessPiece)
            {
                chessPiece.RemoveChessPiece();
            }
        }
        public void MoveChessPiece(Cell orgCell, Cell destCell)
        {
            destCell.MoveChessPiece(orgCell.ChessPiece, this);
            orgCell.RemoveChessPiece();
        }
        public bool FindSelectedCell(out Cell cell)
        {
            var allChessPiece = this.GetAllChessPieces();
            var selectedPiece = allChessPiece.Single(x => x.ChessPiece.IsSelected == true);
            if(this.FindSpecificCell(selectedPiece.X,selectedPiece.Y, out cell))
            {
                return true;
            }
            cell = null;
            return false;
        }
        public bool FindSpecificCell(int x, int y, out Cell cell)
        {
            if(x < 0 || y < 0 || x > GlobalVariables.BoardSizeX - 1 || y > GlobalVariables.BoardSizeY - 1)
            {
                cell = null;
                return false;
            }
            try
            {
                var allCells = this.GetAllCellsInOneList();
                cell = allCells.Single(c => c.X == x && c.Y == y);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DisableAllPieces()
        {
            var allChessPieces = this.GetAllChessPieces();
            foreach(var piece in allChessPieces)
            {
                piece.ChessPiece.CanMove = false;
            }
        }
        public void EnableMoveAblePieces(Side side)
        {
            var allChessPieces = this.GetAllChessPieces();
            foreach(var piece in allChessPieces)
            {
                if(piece.Side == side)
                {
                    piece.ChessPiece.CanMove = true;
                }
                else
                {
                    piece.ChessPiece.CanMove = false;
                }
            }
        }
        public bool CheckWinner(out Side side)
        {
            var allChessPieces = this.GetAllChessPieces();
            var allGenerals = allChessPieces.Where(x => x.ChessPiece.GetChessPieceType() == ChessPieceType.General);
            if(allGenerals.Count() < 2)
            {
                side = allGenerals.Select(g => g.ChessPiece.Side).Single();
                return true;
            }
            side = Side.Red;
            return false;
        }
        public void ShowValidMoves(List<Point> validMovePositions)
        {
            var allCells = this.GetAllCellsInOneList();
            foreach(var cell in allCells)
            {
                foreach (var move in validMovePositions)
                {
                    if(cell.X == move.X && cell.Y == move.Y)
                    {
                        cell.ValidMove.IsValidMove();
                    }
                }
            }
        }
        public void ClearAllSelection()
        {
            var allChessPiece = this.GetAllChessPieces();
            foreach(var piece in allChessPiece)
            {
                piece.ChessPiece.IsSelected = false;
            }
        }
        public void ClearAllValidMove()
        {
            var allCells = this.GetAllCellsInOneList();
            foreach(var cell in allCells)
            {
                cell.ValidMove.NotValidMove();
            }
        }
    }
}
