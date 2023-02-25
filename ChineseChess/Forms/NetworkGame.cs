using GameClient;
using GameCommons;
using GameServer;
using NetworkCommons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess.Forms
{
    public partial class NetworkGame : Form, IClientObserver
    {
        AsynchronousTCPListener listener;
        AsynchronousClient client;
        bool gameStarted = false;
        bool host;
        Side playerSide;
        ChessBoard board;
        Side moveSide;
        List<PictureBox> chessPiecePics = new List<PictureBox>();
        int currentTurn = 1;
        List<Turn> turnRecord = new List<Turn>();
        public NetworkGame()
        {
            InitializeComponent();
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            this.board = new ChessBoard();
            listener = new AsynchronousTCPListener();
            ServerStartAsync();
            ClientStartAsync(NetworkCommons.IP.GetCurrentMachineIP());
            this.moveSide = RandomStart();
            this.playerSide= RandomStart();
            LoadTurn(new Turn(moveSide));
            SendDataToServerAsync(this.playerSide.ToString());
            SendDataToServerAsync(this.turnRecord.First());
        }
        public NetworkGame(string connectionIP)
        {
            InitializeComponent();
            UtilOps.CheckSaveDirectory();
            UtilOps.ClearTempFolder();
            this.board = new ChessBoard();
            ClientStartAsync(connectionIP);

        }
        private async void NetworkGame_Load(object sender, EventArgs e)
        {
            await CheckGameStart();
        }

        #region Network Management

        /// <summary>
        /// Start server
        /// </summary>
        private async void ServerStartAsync()
        {
            var listen = listener.StartListeningAsync();
            await listen;
        }

        /// <summary>
        /// Connect client to server
        /// </summary>
        /// <param name="serverIP"></param>
        private async void ClientStartAsync(string serverIP)
        {
            try
            {
                // ip address of current dervice
                string localIP = NetworkCommons.IP.GetCurrentMachineIP();
                client = new AsynchronousClient(serverIP);
                var connectServer = client.ConnectAsync(serverIP);
                client.RegisterObserver(this);
                await connectServer;
            }
            catch
            {
                Debug.WriteLine($"{serverIP} client connection failed");
            }
        }

        public void OnTcpDataReceived(object data)
        {
            if (data as Turn != null)
            {
                LoadTurn(data as Turn);
            }
            else
            {
                var recievedSide = (Side)Enum.Parse(typeof(Turn), data.ToString());
                if (this.host)
                {
                    this.playerSide= recievedSide;
                }
                else
                {
                    if(recievedSide == Side.Red)
                    {
                        this.playerSide= Side.Black;
                    }
                    else
                    {
                        this.playerSide= Side.Red;
                    }
                }
            }
            Debug.WriteLine($"Observer Received data: {data}");
        }
        private async void SendDataToServerAsync(object data) 
        {
            var sendData = client.SendMessageAsync(data);
            await sendData;
        }

        private Task CheckGameStart()
        {
            while (true)
            {
                if (!gameStarted)
                {
                    this.board.DisableAllPieces();
                }
            }
        }

        #endregion

        #region Turn Management
        private Side RandomStart()
        {
            Random playerStart = new Random();
            //create random number between 0 and 10
            int whoStart = playerStart.Next(10);
            //red starts if smaller than 5, black starts if greater
            if (whoStart < 5)
            {
                 return Side.Red;
            }
            else
            {
                return Side.Black;
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
            SendDataToServerAsync(currentTurnState);
        }

        #endregion

        #region Game Management

        private void LoadTurn(Turn turn)
        {
            this.turnRecord.Add(turn);
            var selectedTurn = this.turnRecord.Last();
            this.ClearBoard();
            this.board.LoadGame(selectedTurn.BoardState);
            this.currentTurn = selectedTurn.TurnNumber;
            this.moveSide = selectedTurn.WhosTurn;
            this.LoadBoardSlate(this.board);
            // player on only use stuff on his/her turn
            if (moveSide == playerSide)
            {
                this.board.EnableMoveAblePieces(this.moveSide);
            }
            else
            {
                this.board.DisableAllPieces();
            }
            this.UpdateTurnLabel();
        }

        private void EndTurn()
        {
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

        #endregion

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

        private void SaveGameMessage()
        {
            MessageBox.Show("Game saved", "Game saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion
    }
}
