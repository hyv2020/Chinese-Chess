using GameCommons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChineseChess
{
    public partial class Game : Form
    {
        ChessBoard board;
        Side moveSide;
        List<PictureBox> chessPiecePics = new List<PictureBox>();
        int currentTurn = 1;
        List<Turn> turnRecord = new List<Turn>();
        public Game(string loadFromFile = null)
        {

            InitializeComponent();
            //initialise the game
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            this.board = new ChessBoard();
            if (loadFromFile is null)
            {
                this.board.LoadGame();
                this.AddAllToControl();
                this.RandomStart();
                this.UpdateTurnLabel();
                this.SaveState();
            }
            else
            {
                this.AddCellsToControl();
                this.LoadSave(loadFromFile);
                this.AddAllTurnsToTurnBox();
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }


        #region Turn Management
        private void RandomStart()
        {
            Random playerStart = new Random();
            //create random number between 0 and 10
            int whoStart = playerStart.Next(10);
            //red starts if smaller than 5, black starts if greater
            if (whoStart < 5)
            {
                this.moveSide = Side.Red;
            }
            else
            {
                this.moveSide = Side.Black;
            }

        }
        private void UpdateTurnLabel(Side? side = null)
        {
            if (side != null)
            {
                if (side == Side.Red)
                {
                    TurnLabel.ForeColor = Color.Red;
                }
                else
                {
                    TurnLabel.ForeColor = Color.Black;
                }
                TurnLabel.Text = $"{side} Side WIns!!!";
                return;
            }
            if (this.moveSide == Side.Red)
            {
                TurnLabel.ForeColor = Color.Red;
            }
            else
            {
                TurnLabel.ForeColor = Color.Black;
            }
            TurnLabel.Text = $"{this.moveSide} Turn";

        }
        private void ChangeSide()
        {
            if (this.moveSide == Side.Red)
            {
                this.moveSide = Side.Black;
            }
            else
            {
                this.moveSide = Side.Red;
            }
        }
        private void AddTurnBoxItem(int turnNumber)
        {
            TurnBox.Items.Add($"Turn {turnNumber}");
        }
        private void AddAllTurnsToTurnBox()
        {
            foreach (var turn in this.turnRecord)
            {
                this.AddTurnBoxItem(turn.TurnNumber);
            }
        }
        #endregion

        #region Controls
        private void AddAllToControl()
        {
            foreach (var col in this.board.Cells)
            {
                foreach (var cell in col)
                {
                    this.Controls.Add(cell.BoardPic);
                    AddedEventHandlerToObjs(cell.BoardPic, cell);
                    this.Controls.Add(cell.ValidMove.ValidMovePicBox);
                    AddedEventHandlerToObjs(cell.ValidMove.ValidMovePicBox, cell.ValidMove);
                    this.AddChessPieceInCellToControl(cell);
                    SortCellImageOrder(cell);
                }
            }
        }
        private void SortCellImageOrder(Cell cell)
        {
            if (cell.ChessPiece != null)
            {
                cell.ChessPiece.ChessPicture.BringToFront();
            }
            cell.ValidMove.ValidMovePicBox.BringToFront();
        }
        private void AddCellsToControl()
        {
            foreach (var col in this.board.Cells)
            {
                foreach (var cell in col)
                {
                    this.Controls.Add(cell.BoardPic);
                    AddedEventHandlerToObjs(cell.BoardPic, cell);
                    this.Controls.Add(cell.ValidMove.ValidMovePicBox);
                    AddedEventHandlerToObjs(cell.ValidMove.ValidMovePicBox, cell.ValidMove);
                    SortCellImageOrder(cell);
                }
            }
        }

        private void AddChessPieceInCellToControl(Cell cell)
        {
            if (cell.ChessPiece != null)
            {
                this.Controls.Add(cell.ChessPiece.ChessPicture);
                this.chessPiecePics.Add(cell.ChessPiece.ChessPicture);
                AddedEventHandlerToObjs(cell.ChessPiece.ChessPicture, cell.ChessPiece);
            }
        }
        #endregion

        #region File Management

        private void LoadSave(string saveFilePath)
        {
            try
            {
                this.turnRecord = UtilOps.LoadSaveFile(saveFilePath).ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to load save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var selectedTurn = this.turnRecord.Last();
            this.ClearBoard();
            this.board.LoadGame(selectedTurn.BoardState);
            this.currentTurn = selectedTurn.TurnNumber;
            this.moveSide = selectedTurn.WhosTurn;
            this.LoadBoardSlate(this.board);
            this.board.EnableMoveAblePieces(this.moveSide);
            this.UpdateTurnLabel();
        }
        private string GetSaveFileName()
        {
            string fileName;
            if (SaveFileNameTextBox.Text == "")
            {
                fileName = $"GameSave";
            }
            else
            {
                fileName = $"{SaveFileNameTextBox.Text}";
            }
            return fileName;
        }
        private void AutoSaveToFile()
        {
            if (AutoSaveBox.Checked)
            {
                string fileName = GetSaveFileName();
                UtilOps.SaveFile(fileName, true);
            }
        }
        private void DeleteTempFilesAfterThisTurn(int currentTurn)
        {
            UtilOps.DeleteTempFilesAfterTurn(currentTurn);
        }
        #endregion

        #region State Management
        private void LoadBoardSlate(ChessBoard chessBoard)
        {
            foreach (var col in chessBoard.Cells)
            {
                foreach (var cell in col)
                {
                    AddChessPieceInCellToControl(cell);
                    SortCellImageOrder(cell);
                }
            }
        }

        private void SaveState()
        {
            Turn currentTurnState = new Turn(this.currentTurn, this.moveSide, this.board.SaveGame().ToList());
            currentTurnState.SaveToFile();
            this.turnRecord.Add(currentTurnState);
            this.AddTurnBoxItem(this.currentTurn);
        }

        #endregion

        private void EndTurn()
        {
            TurnBox.Items.Clear();
            for (int i = 1; i <= this.currentTurn; i++)
            {
                this.AddTurnBoxItem(i);
            }
            var currentTurn = this.turnRecord.FindIndex(x => x.TurnNumber == this.currentTurn);
            var updatedTurnRecord = this.turnRecord.Take(currentTurn + 1);
            this.turnRecord = updatedTurnRecord.ToList();
            this.currentTurn++;
        }
        private void ClearBoard()
        {
            foreach (var pic in this.chessPiecePics)
            {
                this.Controls.Remove(pic);
            }
            this.board.ClearBoard();
            this.chessPiecePics.Clear();
        }

        #region Events and Handler
        private void AddedEventHandlerToObjs<T>(PictureBox pictureBox, T obj)
        {
            var type = obj.ToString();
            string cellType = typeof(Cell).ToString();
            string chessType = typeof(ChessPiece).ToString();
            string vmType = typeof(ValidMove).ToString();
            if (type == cellType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ChessBoard_Click(sender, e));
            }
            else if (type == chessType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ChessPiece_Click(sender, e));
            }
            else if (type == vmType)
            {
                pictureBox.Click += new EventHandler((sender, e) => ValidMove_Click(sender, e));
            }
            else
            {
                throw new Exception();
            }
        }

        private void ChessBoard_Click(object sender, EventArgs e)
        {
            this.board.ClearAllValidMove();
        }
        private void ValidMove_Click(object sender, EventArgs e)
        {
            var pic = (PictureBox)sender;
            var boxName = pic.Name;
            var boxNum = Regex.Match(boxName, @"\d+").Value;
            int[] xy = boxNum.Select(num => int.Parse(num.ToString())).ToArray();

            int x = xy[0];
            int y = xy[1];
            if (this.board.FindSpecificCell(x, y, out var cell))
            {
                if (this.board.FindSelectedCell(out var selectedCell))
                {
                    this.Controls.Remove(selectedCell.ChessPiece.ChessPicture);
                    this.board.MoveChessPiece(selectedCell, cell);
                    this.Controls.Add(cell.ChessPiece.ChessPicture);
                    AddedEventHandlerToObjs(cell.ChessPiece.ChessPicture, cell.ChessPiece);
                    this.board.ClearAllSelection();
                    this.board.ClearAllValidMove();
                    this.SortCellImageOrder(cell);
                    if (this.board.CheckWinner(out Side winner))
                    {
                        MessageBox.Show($"{winner} Side Wins", "We have a winner", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.board.DisableAllPieces();
                        this.UpdateTurnLabel(winner);
                    }
                    else
                    {
                        this.ChangeSide();
                        this.EndTurn();
                        this.SaveState();
                        this.board.EnableMoveAblePieces(this.moveSide);
                        this.UpdateTurnLabel();
                    }
                    this.DeleteTempFilesAfterThisTurn(this.currentTurn);
                    this.AutoSaveToFile();
                }
            }
        }
        private void ChessPiece_Click(object sender, EventArgs e)
        {
            this.board.ClearAllSelection();
            this.board.ClearAllValidMove();
            var allCells = this.board.GetAllCellsInOneList();
            var cellWithChessPiece = allCells.Where(x => x.ChessPiece != null);
            var currentCell = cellWithChessPiece.Single(x => x.ChessPiece.ChessPicture == (PictureBox)sender);
            if (currentCell.ChessPiece.Side == this.moveSide)
            {
                currentCell.ChessPiece.IsSelected = true;
                var validMoves = currentCell.ChessPiece.FindValidMove(this.board);
                this.board.ShowValidMoves(validMoves);
            }
        }
        //quit game
        private void QuitButton_Click(object sender, EventArgs e)
        {
            UtilOps.ClearTempFolder();
            System.Windows.Forms.Application.Exit();
        }

        private int GetTurnNumber(ComboBox turnBox)
        {
            return Convert.ToInt32(turnBox.SelectedItem.ToString().Split(' ').Last());
        }
        private void TurnBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.board.ClearAllSelection();
            this.board.ClearAllValidMove();
            var box = (ComboBox)sender;
            int turnNumber = GetTurnNumber(box);
            var selectedTurn = this.turnRecord.Where(x => x.TurnNumber == turnNumber).FirstOrDefault();
            this.ClearBoard();
            this.board.LoadGame(selectedTurn.BoardState);
            this.currentTurn = selectedTurn.TurnNumber;
            this.moveSide = selectedTurn.WhosTurn;
            this.LoadBoardSlate(this.board);
            this.board.EnableMoveAblePieces(this.moveSide);
            this.UpdateTurnLabel();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName();
            if (System.IO.File.Exists(FilePaths.rootSaveFilePath + fileName + ".sav"))
            {
                var overwrite = MessageBox.Show("Save file exist. Overwrite existing file?", "Overwrite file", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (overwrite == DialogResult.Yes)
                {
                    UtilOps.SaveFile(fileName, true);
                    SaveGameMessage();
                }
                else if (overwrite == DialogResult.No)
                {
                    UtilOps.SaveFile(fileName, false);
                    SaveGameMessage();
                }
            }
            else
            {
                UtilOps.SaveFile(fileName, false);
                SaveGameMessage();
            }
        }
        private void SaveGameMessage()
        {
            MessageBox.Show("Game saved", "Game saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void RestartButton_Click(object sender, EventArgs e)
        {
            UtilOps.ClearTempFolder();
            this.board.ClearAllSelection();
            this.board.ClearAllValidMove();
            this.turnRecord = this.turnRecord.Where(x => x.TurnNumber == 1).ToList();
            var selectedTurn = this.turnRecord.FirstOrDefault();
            this.ClearBoard();
            this.board.LoadGame(selectedTurn.BoardState);
            this.currentTurn = selectedTurn.TurnNumber;
            this.moveSide = selectedTurn.WhosTurn;
            this.LoadBoardSlate(this.board);
            this.board.EnableMoveAblePieces(this.moveSide);
            this.UpdateTurnLabel();
            TurnBox.Items.Clear();
            this.SaveState();
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = FilePaths.rootSaveFilePath;
            openFileDialog.Title = GlobalVariables.LoadDialogTitle;
            openFileDialog.Filter = GameCommons.DefaultVariables.LoadDialogFliter;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                UtilOps.ClearTempFolder();
                string saveFileName = openFileDialog.FileName;
                this.LoadSave(saveFileName);
                TurnBox.Items.Clear();
                this.AddAllTurnsToTurnBox();
            }
        }

        #endregion

    }
}
