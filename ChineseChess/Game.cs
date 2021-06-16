using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace ChineseChess
{
    public partial class Game : Form
    {
        //size of each square
        private const int cellSize = 65;
        //size of chess pieces
        private const int chessSize = 55;
        //size of legal move indicator
        private const int legalMoveBoxSize = 25;

        //extra padding to make everything snip to grid
        private const int chessToCell = (cellSize - chessSize) / 2;
        private const int indicatorToCell = (cellSize - legalMoveBoxSize) / 2;

        //board size
        private const int boardSizeX = 9;
        private const int boardSizeY = 10;
        //board location
        private readonly int boardLocationX = 25;
        private readonly int boardLocationY = 25;

        //turn switch
        private static bool turnSwitch;
        //make the pieces list. all the pieces are stored here.
        //static because the number of players dont change
        public static List<ChessPieceProperties> chessPieceList = new List<ChessPieceProperties>();

        //make a list for different type of chess piece
        //because the piece list do not have attribute from the sub-class
        public static List<Soldier> soldierList = new List<Soldier>();
        public static List<Horse> horseList = new List<Horse>();
        public static List<Minister> ministerList = new List<Minister>();
        public static List<Advisor> advisorList = new List<Advisor>();
        public static List<Cannon> cannonList = new List<Cannon>();
        public static List<Chariot> chariotList = new List<Chariot>();
        public static List<General> generalList = new List<General>();

        //list to store chess piece images
        public static List<PictureBox> chessPiecePicsList = new List<PictureBox>();
        //list to store legal move indicator
        public List<PictureBox> legalMoveBoxList = new List<PictureBox>();
        //make the board, it is static because there is only one board
        //the board size defined by boardsizeX and boardSizeY so 9*10
        public static Board myBoard = new Board(boardSizeX, boardSizeY);

        //number of each chess piece on each side
        public static int numberOfSoldiers = 5;
        public static int numberOfHorses = 2;
        public static int numberOfMinisters = 2;
        public static int numberOfAdvisors = 2;
        public static int numberOfCannons = 2;
        public static int numberOfChariots = 2;

        //set the root location of board images
        public string rootBoardImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\board images\");
        //set the root location of chess piece images
        public string rootChessImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"images\chess piece images\");

        //timer for the game to update
        private static System.Timers.Timer aTimer;
        public Game()
        {
            InitializeComponent();
            //initialise the game

            //display empty board
            DrawBoard(boardLocationX, boardLocationY, cellSize);

            //create the 2d array for board with starting position
            //place the pieces in their starting position
            MakeBoard();

            //draw the chess piece
            DrawChessPiece(chessPiecePicsList, chessPieceList, chessSize);

            //draw and hide the legal move indicator
            LegalMoveIndictor(legalMoveBoxSize, legalMoveBoxList);
        }

        private void Game_Load(object sender, EventArgs e)
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += GameUpdate;
            aTimer.Enabled = true;
            //turn-based mechanic
            //rules of the board
            //set advisor area and river

            OccupiedCellUpdate(myBoard);
            //random number to decide who starts first
            Random playerStart = new Random();
            //create random number between 0 and 10
            int whoStart = playerStart.Next(10);
            //red starts if smaller than 5, black starts if greater
            if (whoStart < 5)
            {
                turnSwitch = true;
            }
            else
            {
                turnSwitch = false;
            }
        }
        private void GameUpdate(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (turnSwitch == true)
            {
                //Application.DoEvents();
                TurnLabel.ForeColor = Color.Black;
                SetText("Black Turn");
            }
            else
            {
                //Application.DoEvents();
                TurnLabel.ForeColor = Color.Red;
                SetText("Red Turn");
            }
            SideControl();
            //win condition
            while (generalList.Count < 2)
            {
                aTimer.Stop();
                if (generalList[0].PieceColour == ChessPieceProperties.SideColour.Red)
                {
                    MessageBox.Show("Red Wins", "We have a winner!");
                    TurnLabel.ForeColor = Color.Red;
                    SetText("RED WINS!");
                    for (int j = 0; j < chessPieceList.Count; j++)
                    {
                        chessPieceList[j].WasMoved = true;
                    }
                    break;
                }
                else
                {
                    MessageBox.Show("Black Wins", "We have a winner!");
                    TurnLabel.ForeColor = Color.Black;
                    SetText("BLACK WINS!");
                    for (int j = 0; j < chessPieceList.Count; j++)
                    {
                        chessPieceList[j].WasMoved = true;
                    }
                    break;
                }
            }
        }
        delegate void LabelText(string text);
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.TurnLabel.InvokeRequired)
            {
                LabelText d = new LabelText(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.TurnLabel.Text = text;
            }
        }
        private void TestBoard()
        {
            //test to scan the board and display it in console
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int y = 0; y < boardSizeY; y++)
                {
                    if (myBoard.TheGrid[x, y].SelectedCell == true)
                    {
                        Console.Write("o");
                    }

                    else if (myBoard.TheGrid[x, y].LegalNextMove == true)
                    {
                        Console.Write("x");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("-----------------");
            Console.WriteLine();
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int y = 0; y < boardSizeY; y++)
                {
                    if (myBoard.TheGrid[x, y].CurrentlyOccupied == true)
                    {
                        Console.Write("O");
                        for (int k = 0; k < chessPieceList.Count; k++)
                        {
                            if (chessPieceList[k].PieceX == x && chessPieceList[k].PieceY == y)
                            {
                                //Console.Write(chessPieceList[k].name);
                            }
                        }
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            Console.WriteLine("-----------------");
            Console.WriteLine();
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].IsSelected)
                {
                    Console.WriteLine(chessPieceList[i].name);
                    Console.WriteLine(chessPieceList[i].PieceColour);
                    Console.WriteLine(chessPieceList[i].PieceType);
                    Console.WriteLine();
                    for (int j = 0; j < chessPiecePicsList.Count; j++)
                    {
                        if (chessPiecePicsList[j].Name == chessPieceList[i].name)
                        {
                            Console.WriteLine(chessPiecePicsList[j].Name);
                        }
                    }
                }
            }
        }

        //make the board
        private void MakeBoard()
        {
            //create chess pieces in their starting position
            //make new object to use function
            //use function to create pieces for the game
            //different function creates different types of chess piece

            //create soldier
            Soldier newSoldier = new Soldier();
            newSoldier.MakeSoldierPiece(numberOfSoldiers, boardSizeX, boardSizeY,
                myBoard, soldierList, chessPieceList);
            //create horse
            Horse newHorse = new Horse();
            newHorse.MakeHorsePiece(numberOfHorses, boardSizeX, boardSizeY,
                myBoard, horseList, chessPieceList);
            //create minister
            Minister newMinster = new Minister();
            newMinster.MakeMinisterPiece(numberOfMinisters, boardSizeX, boardSizeY,
                myBoard, ministerList, chessPieceList);
            //create advisor
            Advisor newAdvisor = new Advisor();
            newAdvisor.MakeAdvisorPiece(numberOfAdvisors, boardSizeX, boardSizeY,
                myBoard, advisorList, chessPieceList);
            //create cannon
            Cannon newCannon = new Cannon();
            newCannon.MakeCannonPiece(numberOfCannons, boardSizeX, boardSizeY,
                myBoard, cannonList, chessPieceList);
            //create chariot
            Chariot newChariot = new Chariot();
            newChariot.MakeChariotPiece(numberOfChariots, boardSizeX, boardSizeY,
                myBoard, chariotList, chessPieceList);
            //create general
            General newGeneral = new General();
            newGeneral.MakeGeneralPiece(boardSizeX, boardSizeY,
                myBoard, generalList, chessPieceList);
        }

        //select a cell to match the selected piece location
        private void OccupiedCellUpdate(Board chessBoard)
        {
            //scan the chess piece list and the board
            //scan chess piece list
            //scan the board for occupied cell
            for (int k = 0; k < chessPieceList.Count; k++)
            {
                //scan the board
                for (int i = 0; i < boardSizeX; i++)
                {
                    for (int j = 0; j < boardSizeY; j++)
                    {
                        //if there is a piece in that cell, it is occupied
                        chessBoard.TheGrid[chessPieceList[k].PieceX,
                            chessPieceList[k].PieceY].CurrentlyOccupied = true;
                        //create the advisor area on both sides
                        if (i > 2 && i < 6 && j < 3 || i > 2 && i < 6 && j > boardSizeY - 4)
                        {
                            chessBoard.TheGrid[i, j].AdviserArea = true;
                        }
                        else
                        {
                            myBoard.TheGrid[i, j].AdviserArea = false;
                        }
                    }
                }
            }
        }

        //update board selection based on piece status
        private void UpdateBoardSelection()
        {
            for (int k = 0; k < chessPieceList.Count; k++)
            {
                //checks selection
                if (chessPieceList[k].IsSelected == true)
                {
                    myBoard.TheGrid[chessPieceList[k].PieceX,
                    chessPieceList[k].PieceY].SelectedCell = true;
                }
                else if (chessPieceList[k].IsSelected == false)
                {
                    myBoard.TheGrid[chessPieceList[k].PieceX,
                    chessPieceList[k].PieceY].SelectedCell = false;
                }
            }
        }
        //update board based on piece location
        private void UpdatePiecePic(List<ChessPieceProperties> chessPieceList, List<PictureBox> chessPiecePicsList)
        {
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Red)
                {
                    RedPiecePicsLocation(i, chessPieceList, chessPiecePicsList);
                }
                else if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Black)
                {
                    BlackPiecePicsLocation(i, chessPieceList, chessPiecePicsList);
                }
            }

        }
        //function to draw the board
        private void DrawBoard(int locationX, int locationY, int cellSize)
        {
            //pictures for the board
            List<PictureBox> pictureBoxList = new List<PictureBox>();
            //default location of the board
            int boardLocationX = locationX;
            int boardLocationY = locationY;
            //size of each cell
            int pictureSize = cellSize;
            //fill the board with pictureboxes
            for (int i = 0; i < boardSizeX; i++)
            {
                for (int j = 0; j < boardSizeY; j++)
                {
                    //show each cell as picturebox
                    //make new picturebox call chessCell
                    PictureBox chessCell = new PictureBox
                    {
                        //properties of picturebox
                        //change name of the picturebox
                        Name = "boardTile" + i + j,
                        //size of the box
                        Size = new Size(pictureSize, pictureSize),
                        //colour of the box
                        BackColor = Color.Blue,
                        //the location changes to make the whole board
                        Location = new Point(boardLocationX + i * (pictureSize),
                        boardLocationY + j * (pictureSize)),
                        //border style
                        BorderStyle = BorderStyle.None,
                        //stretch image to fit in box
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        //show the box
                        Visible = true,
                    };
                    //add the picturebox
                    this.Controls.Add(chessCell);
                    Controls.SetChildIndex(chessCell, 0);
                    //click event of the board
                    chessCell.Click += ChessCell_Click;
                    //add it to the picturebox list
                    pictureBoxList.Add(chessCell);

                    //draw the board with pictureBox tiles
                    //draw the top edge and adviser area
                    DrawTopEdge(chessCell, i, j);
                    //draw the bottom edge and adviser area
                    DrawBottomEdge(chessCell, i, j);
                    //draw the sides
                    DrawSideEdge(chessCell, i, j);
                    //draw the river
                    DrawRiver(chessCell, i, j);
                    //fill the rest of the board with tiles
                    DrawCentreTile(chessCell);
                }
            }
        }
        //clear selection if click on the board
        private void ChessCell_Click(object sender, EventArgs e)
        {
            ClearSelection();
        }

        private void DrawTopEdge(PictureBox chessCell, int x, int y)
        {
            //draw top edge

            // draw left side of top edge
            if (x > 0 && x < 3 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            //draw the middle bit where the general is
            else if (x == 4 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            // draw right side of top edge
            else if (x > 5 && x < boardSizeX - 1 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }

            //draw top left corner
            else if (x == 0 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top left.gif");
            }

            //draw top right corner
            else if (x == boardSizeX - 1 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top right.gif");
            }

            //draw top advisor area
            //left of area
            else if (x == 3 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area left edge.gif");
            }
            //right of area
            else if (x == 5 && y == 0)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area right edge.gif");
            }
            //the centre of area
            else if (x == 4 && y == 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile advisor area centre.gif");
            }
            //bottom left of area
            else if (x == 3 && y == 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area bottom left.gif");
            }
            //bottom right of area
            else if (x == 5 && y == 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top advisor area bottom right.gif");
            }
        }

        private void DrawBottomEdge(PictureBox chessCell, int x, int y)
        {
            // draw left side of bottom edge
            if (x > 0 && x < 3 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            //draw the middle bit where the general is
            else if (x == 4 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            // draw right side of bottom edge
            else if (x > 5 && x < boardSizeX - 1 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }

            //draw bottom left corner
            else if (x == 0 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom left.gif");
            }

            //draw bottom right corner
            else if (x == boardSizeX - 1 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom right.gif");
            }

            //draw bottom advisor area
            //left of area
            else if (x == 3 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area left edge.gif");
            }
            //right of area
            else if (x == 5 && y == boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area right edge.gif");
            }
            //the centre of area
            else if (x == 4 && y == boardSizeY - 2)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile advisor area centre.gif");
            }
            //top left of area
            else if (x == 3 && y == boardSizeY - 3)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area top left.gif");
            }
            //top right of area
            else if (x == 5 && y == boardSizeY - 3)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom advisor area top right.gif");
            }
        }

        private void DrawSideEdge(PictureBox chessCell, int x, int y)
        {
            //draw left edge
            if (x == 0 && y > 0 && y < boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile left edge.gif");
            }

            //draw right edge
            else if (x == boardSizeX - 1 && y > 0 && y < boardSizeY - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile right edge.gif");
            }
        }

        private void DrawRiver(PictureBox chessCell, int x, int y)
        {
            //top side of river
            if (y == 4 && x > 0 && x < boardSizeX - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile bottom edge.gif");
            }
            //bottom side of river
            else if (y == 5 && x > 0 && x < boardSizeX - 1)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile top edge.gif");
            }
        }

        private void DrawCentreTile(PictureBox chessCell)
        {
            //fill empty tile with centre tile
            if (chessCell.Image == null)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile.gif");
            }
        }

        //draw the chess piece
        private void DrawChessPiece(List<PictureBox> ChessPiecePicsList,
            List<ChessPieceProperties> ChessPieceList, int chessSize)
        {
            //ideally it could use the location of the cell of the board to snap to the grid
            //there is a loop to determine its location and draw
            //the loop look through ChessPieceList to find out how many it needs to draw
            //the location is set depending on colour and piece type

            //define size of the chess piece
            int pieceSize = chessSize;
            //create a list to store pictureBox

            for (int i = 0; i < ChessPieceList.Count; i++)
            {
                //show each cell as picturebox
                //make new picturebox call chessCell
                PictureBox chessPictureBox = new PictureBox
                {
                    //properties of picturebox
                    //change name of the picturebox
                    //use colour and type of the piece
                    //for example "red horse"
                    Name = ChessPieceList[i].name,
                    //size of the chess piece box
                    Size = new Size(pieceSize, pieceSize),
                    //colour of the box
                    BackColor = Color.Black,
                    //border style
                    BorderStyle = BorderStyle.None,
                    //stretch image to fit in box
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    //show the box
                    Visible = true
                };

                //add the picturebox
                Controls.Add(chessPictureBox);
                Controls.SetChildIndex(chessPictureBox, 1);
                //being it in front of the board
                chessPictureBox.BringToFront();
                //add it to the picturebox list
                ChessPiecePicsList.Add(chessPictureBox);

                //click event for that pictureBox
                //select it when clicked
                string piecePicName = chessPiecePicsList[i].Name;
                chessPiecePicsList[i].Click += new EventHandler((sender, e) => ChessPiece_Click(sender, e, piecePicName, chessPieceList));

                //set the starting location of the piece
                //get the chess piece in list and its location
                //get location depending on side
                //i is the list ID
                if (ChessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Red)
                {
                    //use data from p1 list for red
                    RedPiecePicsLocation(i, chessPieceList, ChessPiecePicsList);
                }
                else
                {
                    //use data from p2 list for black
                    BlackPiecePicsLocation(i, chessPieceList, ChessPiecePicsList);
                }

            }
        }
        //give the chess piece the right image and assign them to their positions
        private void RedPiecePicsLocation(int chessPieceListID, List<ChessPieceProperties> chessPieceList, List<PictureBox> chessPiecePicsList)
        {
            //set location of the image for each piece
            //use data from the picture box
            //the pieces already have location when created in the function
            //add the offset of the board location
            chessPiecePicsList[chessPieceListID].Location = new Point(boardLocationX + chessToCell + chessPieceList[chessPieceListID].PieceX * cellSize,
            boardLocationY + chessToCell + chessPieceList[chessPieceListID].PieceY * cellSize);

            //different image for different piece type
            if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Soldier)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red soldier.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Horse)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red horse.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Minister)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red minister.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Advisor)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red advisor.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Cannon)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red cannon.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Chariot)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red chariot.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.General)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "red general.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
        }
        //same code as red but load different location and pictures
        private void BlackPiecePicsLocation(int chessPieceListID, List<ChessPieceProperties> chessPieceList, List<PictureBox> chessPiecePicsList)
        {
            //use chess list to get location
            chessPiecePicsList[chessPieceListID].Location = new Point(boardLocationX + chessToCell + chessPieceList[chessPieceListID].PieceX * cellSize,
            boardLocationY + chessToCell + chessPieceList[chessPieceListID].PieceY * cellSize);

            //different image for different piece type
            if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Soldier)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black soldier.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Horse)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black horse.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Minister)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black minister.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Advisor)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black advisor.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Cannon)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black cannon.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.Chariot)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black chariot.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
            else if (chessPieceList[chessPieceListID].PieceType == ChessPieceProperties.ChessPieceType.General)
            {
                chessPiecePicsList[chessPieceListID].Image = Image.FromFile(rootChessImageFilePath + "black general.gif");
                chessPiecePicsList[chessPieceListID].BackColor = Color.White;
            }
        }
        // fill the board with move indicators and hide them
        //only shows when there is a legal move
        private void LegalMoveIndictor(int legalMoveBoxSize, List<PictureBox> legalMoveBox)
        {
            for (int i = 0; i < boardSizeX; i++)
            {
                for (int j = 0; j < boardSizeY; j++)
                {
                    PictureBox legalMoveIndicator = new PictureBox
                    {
                        //properties of picturebox
                        //change name of the picturebox
                        //use colour and type of the piece
                        //for example "red horse"
                        Name = "legalMoveIndicator" + i + j,
                        //size of the legal move box
                        Size = new Size(legalMoveBoxSize, legalMoveBoxSize),
                        //colour of the box
                        BackColor = Color.Black,
                        //border style
                        BorderStyle = BorderStyle.None,
                        //stretch image to fit in box
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        //location
                        Location = new Point(boardLocationX + indicatorToCell + i * (cellSize),
                        boardLocationY + indicatorToCell + j * (cellSize)),
                        //hide the box
                        Visible = false
                    };
                    this.Controls.Add(legalMoveIndicator);
                    Controls.SetChildIndex(legalMoveIndicator, 2);
                    legalMoveIndicator.BringToFront();
                    legalMoveBoxList.Add(legalMoveIndicator);
                }
            }
        }
        private void ClearSelection()
        {
            //use for clearing stuff from previous selection
            //hide on indicators
            for (int i = 0; i < legalMoveBoxList.Count; i++)
            {
                legalMoveBoxList[i].Visible = false;
            }
            //clear all selection
            for (int j = 0; j < chessPieceList.Count; j++)
            {
                chessPieceList[j].IsSelected = false;
            }
            //scan the board and clear all legal moves and selected cells
            for (int x = 0; x < boardSizeX; x++)
            {
                for (int y = 0; y < boardSizeY; y++)
                {
                    myBoard.TheGrid[x, y].LegalNextMove = false;
                    myBoard.TheGrid[x, y].SelectedCell = false;
                }

            }
        }
        //check the collision to not move to cell with a piece with the same colour
        private void CheckCollision(List<ChessPieceProperties> chessPieceList, Board chessBoard)
        {
            //to find which colour is selected 
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                ChessPieceProperties.SideColour colourSelected;
                if (chessPieceList[i].IsSelected == true)
                {
                    //set variable for colour selected
                    colourSelected = chessPieceList[i].PieceColour;
                    //scan the board for legal move cells with a piece on
                    for (int x = 0; x < boardSizeX; x++)
                    {
                        for (int y = 0; y < boardSizeX; y++)
                        {
                            //this cell has a legal move and occupied
                            if (chessBoard.TheGrid[x, y].LegalNextMove == true)
                            {
                                //check the piece list for which one with the same location
                                for (int j = 0; j < chessPieceList.Count; j++)
                                {
                                    //set variables for the location of checking cell
                                    int currentCheckingCellX = chessPieceList[j].PieceX;
                                    int currentCheckingCellY = chessPieceList[j].PieceY;
                                    //checking cell's colour
                                    ChessPieceProperties.SideColour currentCheckingCellColour = chessPieceList[j].PieceColour;
                                    //if it has the same colour as the selected cell
                                    if (currentCheckingCellColour == colourSelected)
                                    {
                                        //this cell is not a legal move
                                        chessBoard.TheGrid[currentCheckingCellX, currentCheckingCellY].LegalNextMove = false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        //check the type of piece to find which function to use for legal moves
        private void ShowLegalMoves(List<ChessPieceProperties> chessPieceList, Board chessBoard)
        {
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].WasMoved == false)
                {
                    if (chessPieceList[i].IsSelected == true)
                    {
                        if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Soldier)
                        {
                            ShowSoldierMoves(soldierList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Horse)
                        {
                            ShowHorseMoves(horseList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Minister)
                        {
                            ShowMinsterMoves(ministerList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Advisor)
                        {
                            ShowAdvisorMoves(advisorList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Cannon)
                        {
                            ShowCannonMoves(cannonList, chessBoard);
                            ShowCannonKill(chessPieceList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.Chariot)
                        {
                            ShowChariotMoves(chariotList, chessBoard);
                        }
                        else if (chessPieceList[i].PieceType == ChessPieceProperties.ChessPieceType.General)
                        {
                            ShowGeneralMoves(generalList, chessBoard);
                            FlyingGeneral(chessBoard, chessPieceList);
                        }
                    }
                }
            }
            //check collision with pieces on the same side
            //can not move to their cells
            //if there is a enermy piece, kill it
        }
        //functions to call functions in the piece subclass to get legal moves for different type of pieces
        private void ShowSoldierMoves(List<Soldier> soldierList, Board chessBoard)
        {
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Red)
                {
                    if (chessPieceList[i].PieceY > 4)
                    {
                        chessPieceList[i].CrossRiver = true;
                    }
                    else
                    {
                        chessPieceList[i].CrossRiver = false;
                    }
                }
                else if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Black)
                {
                    if (chessPieceList[i].PieceY < 5)
                    {
                        chessPieceList[i].CrossRiver = true;
                    }
                    else
                    {
                        chessPieceList[i].CrossRiver = false;
                    }
                }
            }
            for (int i = 0; i < soldierList.Count; i++)
            {
                if (soldierList[i].IsSelected == true)
                {
                    soldierList[i].GetSoldierValidMove(chessBoard);
                }
            }
        }
        private void ShowHorseMoves(List<Horse> horseList, Board chessBoard)
        {
            for (int i = 0; i < horseList.Count; i++)
            {
                if (horseList[i].IsSelected == true)
                {
                    horseList[i].GetHorseValidMove(chessBoard);
                }
            }
        }
        private void ShowMinsterMoves(List<Minister> ministerList, Board chessBoard)
        {
            for (int i = 0; i < ministerList.Count; i++)
            {
                if (ministerList[i].IsSelected == true)
                {
                    ministerList[i].GetMinisterValidMove(chessBoard);
                }
            }
        }
        private void ShowAdvisorMoves(List<Advisor> advisorList, Board chessBoard)
        {
            for (int i = 0; i < advisorList.Count; i++)
            {
                if (advisorList[i].IsSelected == true)
                {
                    advisorList[i].GetAdvisorValidMove(chessBoard);
                }
            }
        }
        private void ShowCannonMoves(List<Cannon> cannonList, Board chessBoard)
        {
            for (int i = 0; i < cannonList.Count; i++)
            {
                if (cannonList[i].IsSelected == true)
                {
                    cannonList[i].GetCannonValidMove(chessBoard);
                }
            }
        }
        //set legal move for cannon kill
        //it need a piece in between to take the piece behind it
        private void ShowCannonKill(List<ChessPieceProperties> chessPieceList, Board chessBoard)
        {
            //check selection
            //variables for selected piece location
            int selectedLocationX = 0;
            int selectedLocationY = 0;
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].IsSelected == true)
                {
                    selectedLocationX = chessPieceList[i].PieceX;
                    selectedLocationY = chessPieceList[i].PieceY;
                }
            }
            //x axis moves
            //scan the whole axis to find the cell behind the first occupied cell
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceXSmaller = 0;
            int blockingPieceXGreater = 0;
            //going right
            int occupiedRightCounter = 0;

            for (int i = selectedLocationX + 1; i < chessBoard.SizeX; i++)
            {
                //use a counter so only one cell can be legal
                if (blockingPieceXGreater == 0 && chessBoard.TheGrid[i, selectedLocationY].CurrentlyOccupied == true)
                {
                    blockingPieceXGreater = i;
                    occupiedRightCounter += 1;
                }
                //if counter is 1 and it is occupied, this cell is legal
                else if (occupiedRightCounter == 1 && blockingPieceXGreater != 0 && i > blockingPieceXGreater
                    && chessBoard.TheGrid[i, selectedLocationY].CurrentlyOccupied == true)
                {
                    chessBoard.TheGrid[i, selectedLocationY].LegalNextMove = true;
                    //increase counter by 1 so no more cell can be legal
                    occupiedRightCounter += 1;
                }
            }

            //if its already occupied
            //the cells behind them are not legal
            //going left
            int occupiedLeftCounter = 0;

            for (int j = selectedLocationX - 1; j >= 0; j--)
            {
                if (blockingPieceXSmaller == 0 && chessBoard.TheGrid[j, selectedLocationY].CurrentlyOccupied == true)
                {
                    blockingPieceXSmaller = j;
                    occupiedLeftCounter += 1;
                }
                if (occupiedLeftCounter == 1 && blockingPieceXSmaller != 0 && j < blockingPieceXSmaller
                    && chessBoard.TheGrid[j, selectedLocationY].CurrentlyOccupied == true)
                {
                    chessBoard.TheGrid[j, selectedLocationY].LegalNextMove = true;
                    occupiedLeftCounter += 1;
                }
            }
            //for the y axis
            //scan the whole axis to find the cell behind the first occupied cell
            //if its already occupied
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceYSmaller = 0;
            int blockingPieceYGreater = 0;
            //going down
            int occupiedDownCounter = 0;

            for (int i = selectedLocationY + 1; i < chessBoard.SizeY; i++)
            {
                if (blockingPieceYGreater == 0 && chessBoard.TheGrid[selectedLocationX, i].CurrentlyOccupied == true)
                {
                    blockingPieceYGreater = i;
                    occupiedDownCounter += 1;
                }
                if (occupiedDownCounter == 1 && blockingPieceYGreater != 0 && i > blockingPieceYGreater
                    && chessBoard.TheGrid[selectedLocationX, i].CurrentlyOccupied == true)
                {
                    chessBoard.TheGrid[selectedLocationX, i].LegalNextMove = true;
                    occupiedDownCounter += 1;
                }

            }

            //if its already occupied
            //the cells behind them are not legal
            //going up
            int occupiedUpCounter = 0;

            for (int j = selectedLocationY - 1; j >= 0; j--)
            {
                if (blockingPieceYSmaller == 0 && chessBoard.TheGrid[selectedLocationX, j].CurrentlyOccupied == true)
                {
                    blockingPieceYSmaller = j;
                    occupiedUpCounter += 1;
                }
                if (occupiedUpCounter == 1 && blockingPieceYSmaller != 0 && j < blockingPieceYSmaller
                    && chessBoard.TheGrid[selectedLocationX, j].CurrentlyOccupied == true)
                {
                    chessBoard.TheGrid[selectedLocationX, j].LegalNextMove = true;
                    occupiedUpCounter += 1;
                }
            }

        }

        private void ShowChariotMoves(List<Chariot> chariotList, Board chessBoard)
        {
            for (int i = 0; i < chariotList.Count; i++)
            {
                if (chariotList[i].IsSelected == true)
                {
                    chariotList[i].GetChariotMove(chessBoard);
                }
            }
        }
        private void ShowGeneralMoves(List<General> generalList, Board chessBoard)
        {
            for (int i = 0; i < generalList.Count; i++)
            {
                if (generalList[i].IsSelected == true)
                {
                    generalList[i].GetGeneralValidMove(chessBoard);
                }
            }
        }
        private void FlyingGeneral(Board chessBoard, List<ChessPieceProperties> chessPieceList)
        {
            int selectedLocationX = 0;
            int selectedLocationY = 0;
            int checkingUpCounter = 0;
            int checkingDownCounter = 0;
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (chessPieceList[i].IsSelected == true)
                {
                    selectedLocationX = chessPieceList[i].PieceX;
                    selectedLocationY = chessPieceList[i].PieceY;
                    //going down
                    for (int y = selectedLocationY + 1; y < chessBoard.SizeY; y++)
                    {
                        if (chessBoard.TheGrid[selectedLocationX, y].CurrentlyOccupied == true)
                        {
                            int occupiedY = y;
                            if (checkingUpCounter == 0)
                            {
                                for (int j = 0; j < chessPieceList.Count; j++)
                                {
                                    int checkingCellX = chessPieceList[j].PieceX;
                                    int checkingCellY = chessPieceList[j].PieceY;
                                    ChessPieceProperties.ChessPieceType checkingType = chessPieceList[j].PieceType;
                                    if (selectedLocationX == checkingCellX && occupiedY == checkingCellY && checkingType == ChessPieceProperties.ChessPieceType.General)
                                    {
                                        chessBoard.TheGrid[checkingCellX, checkingCellY].LegalNextMove = true;
                                    }
                                }
                            }
                            checkingUpCounter += 1;
                        }
                    }
                    //going up
                    for (int y = selectedLocationY - 1; y >= 0; y--)
                    {
                        if (chessBoard.TheGrid[selectedLocationX, y].CurrentlyOccupied == true)
                        {
                            int occupiedY = y;
                            if (checkingDownCounter == 0)
                            {
                                for (int j = 0; j < chessPieceList.Count; j++)
                                {
                                    int checkingCellX = chessPieceList[j].PieceX;
                                    int checkingCellY = chessPieceList[j].PieceY;
                                    ChessPieceProperties.ChessPieceType checkingType = chessPieceList[j].PieceType;
                                    if (selectedLocationX == checkingCellX && occupiedY == checkingCellY && checkingType == ChessPieceProperties.ChessPieceType.General)
                                    {
                                        chessBoard.TheGrid[checkingCellX, checkingCellY].LegalNextMove = true;
                                    }
                                }
                            }
                            checkingDownCounter += 1;
                        }
                    }
                }
            }
        }

        //show the correct indicators for legal moves
        private void ShowIndicator(List<PictureBox> legalMoveBoxList, Board chessBoard)
        {
            for (int k = 0; k < legalMoveBoxList.Count; k++)
            {
                for (int i = 0; i < boardSizeX; i++)
                {
                    for (int j = 0; j < boardSizeY; j++)
                    {
                        if (chessBoard.TheGrid[i, j].LegalNextMove == true)
                        {
                            if (legalMoveBoxList[k].Name == "legalMoveIndicator" + i + j)
                            {
                                legalMoveBoxList[k].Visible = true;
                                //click event
                                string legalBoxName = legalMoveBoxList[k].Name;
                                legalMoveBoxList[k].Click += new EventHandler((sender, e) =>
                                MovePiece(sender, e, legalBoxName, legalMoveBoxList,
                                chessPieceList, chessPiecePicsList, chessBoard));

                            }
                        }
                    }
                }
            }
        }

        //this runs when click on a legal move indicator
        //move the chess piece to legal cell after clicking on it
        private void MovePiece(object sender, EventArgs e, string legalBoxName,
            List<PictureBox> legalMoveBoxList, List<ChessPieceProperties> chessPieceList,
            List<PictureBox> chessPiecePicsList, Board chessBoard)
        {
            //check to kill first
            //set location of the choosen legal move cell to current cell
            //look through legal indicator list to find which it is moving to
            for (int i = 0; i < legalMoveBoxList.Count; i++)
            {
                //find which one to move to
                //if the name matches with the choosen one
                if (legalMoveBoxList[i].Name == legalBoxName)
                {
                    //find and set move location variables
                    int choosenMoveLocationX = (legalMoveBoxList[i].Location.X - indicatorToCell - boardLocationX) / cellSize;
                    int choosenMoveLocationY = (legalMoveBoxList[i].Location.Y - indicatorToCell - boardLocationY) / cellSize;
                    //if it is occupied use location to find who to kill
                    //look through the chess piece list to find which one is selected to move
                    for (int j = 0; j < chessPieceList.Count; j++)
                    {

                        //if it is selected
                        if (chessPieceList[j].IsSelected == true)
                        {
                            //the current position is not long occurpied
                            //clear selection of current on board
                            int selectedCellLocationX = chessPieceList[j].PieceX;
                            int selectedCellLocationY = chessPieceList[j].PieceY;
                            ChessPieceProperties selectedPiece = chessPieceList[j];
                            //the current location is no longer occupied and selected
                            chessBoard.TheGrid[selectedCellLocationX, selectedCellLocationY].CurrentlyOccupied = false;
                            chessBoard.TheGrid[selectedCellLocationX, selectedCellLocationY].SelectedCell = false;

                            //kill if there is enermy piece
                            if (chessBoard.TheGrid[choosenMoveLocationX, choosenMoveLocationY].CurrentlyOccupied == true)
                            {
                                //remove it from all list
                                string pieceToKill;
                                for (int k = 0; k < chessPieceList.Count; k++)
                                {
                                    if (chessPieceList[k].IsSelected == false && chessPieceList[k].PieceX == choosenMoveLocationX && chessPieceList[k].PieceY == choosenMoveLocationY)
                                    {
                                        pieceToKill = chessPieceList[k].name;
                                        if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Soldier)
                                        {
                                            for (int l = 0; l < soldierList.Count; l++)
                                            {
                                                if (soldierList[l].name == pieceToKill)
                                                {
                                                    soldierList.Remove(soldierList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Horse)
                                        {
                                            for (int l = 0; l < horseList.Count; l++)
                                            {
                                                if (horseList[l].name == pieceToKill)
                                                {
                                                    horseList.Remove(horseList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Minister)
                                        {
                                            for (int l = 0; l < ministerList.Count; l++)
                                            {
                                                if (ministerList[l].name == pieceToKill)
                                                {
                                                    ministerList.Remove(ministerList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Advisor)
                                        {
                                            for (int l = 0; l < advisorList.Count; l++)
                                            {
                                                if (advisorList[l].name == pieceToKill)
                                                {
                                                    advisorList.Remove(advisorList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Cannon)
                                        {
                                            for (int l = 0; l < cannonList.Count; l++)
                                            {
                                                if (cannonList[l].name == pieceToKill)
                                                {
                                                    cannonList.Remove(cannonList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.Chariot)
                                        {
                                            for (int l = 0; l < chariotList.Count; l++)
                                            {
                                                if (chariotList[l].name == pieceToKill)
                                                {
                                                    chariotList.Remove(chariotList[l]);
                                                }
                                            }
                                        }
                                        else if (chessPieceList[k].PieceType == ChessPieceProperties.ChessPieceType.General)
                                        {
                                            for (int l = 0; l < generalList.Count; l++)
                                            {
                                                if (generalList[l].name == pieceToKill)
                                                {
                                                    generalList.Remove(generalList[l]);
                                                }
                                            }
                                        }
                                        //hide and remove the picture
                                        for (int m = 0; m < chessPiecePicsList.Count; m++)
                                        {
                                            if (chessPiecePicsList[m].Name == pieceToKill)
                                            {
                                                chessPiecePicsList[m].Visible = false;
                                                chessPiecePicsList.Remove(chessPiecePicsList[m]);
                                            }
                                        }
                                        chessPieceList.Remove(chessPieceList[k]);
                                    }
                                }
                            }
                            //update new location
                            //update position of the piece
                            selectedPiece.PieceX = choosenMoveLocationX;
                            selectedPiece.PieceY = choosenMoveLocationY;
                            chessBoard.TheGrid[choosenMoveLocationX, choosenMoveLocationY].CurrentlyOccupied = true;
                            selectedPiece.IsSelected = false;
                            SideControl();
                            TurnSwitcher();
                        }
                    }
                }
                //clear all legal moves on the board after move
                for (int x = 0; x < boardSizeX; x++)
                {
                    for (int y = 0; y < boardSizeY; y++)
                    {
                        chessBoard.TheGrid[x, y].LegalNextMove = false;
                    }
                }
                UpdatePiecePic(chessPieceList, chessPiecePicsList);
                //hide all legal move indicator
                legalMoveBoxList[i].Visible = false;

            }
        }
        private void SideControl()
        {
            for (int i = 0; i < chessPieceList.Count; i++)
            {
                if (turnSwitch == true)
                {

                    if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Red)
                    {
                        chessPieceList[i].WasMoved = true;
                    }
                    else
                    {
                        chessPieceList[i].WasMoved = false;
                    }
                }
                else
                {
                    if (chessPieceList[i].PieceColour == ChessPieceProperties.SideColour.Black)
                    {
                        chessPieceList[i].WasMoved = true;
                    }
                    else
                    {
                        chessPieceList[i].WasMoved = false;
                    }
                }
            }
        }
        private void TurnSwitcher()
        {
            if (turnSwitch == true)
            {
                turnSwitch = false;
            }
            else
            {
                turnSwitch = true;
            }
        }
        private void ChessPiece_Click(object sender, EventArgs e, string piecePicName, List<ChessPieceProperties> chessPieceList)
        {
            //clear selection first
            ClearSelection();
            //check colour
            //check type
            //show legal moves
            //if click on legal move tile, move the piece
            //if click else where end select
            for (int j = 0; j < chessPieceList.Count; j++)
            {
                if (piecePicName == chessPieceList[j].name)
                {
                    //find matching name object and select it
                    chessPieceList[j].IsSelected = true;
                    //updates the board
                    //select matching cell on board
                    UpdateBoardSelection();
                    //checks the board
                    //calculate all legal move for selected piece
                    ShowLegalMoves(chessPieceList, myBoard);
                    //TestBoard();

                    CheckCollision(chessPieceList, myBoard);

                    //draw the legal move indicators
                    ShowIndicator(legalMoveBoxList, myBoard);
                    break;
                }
                else
                {
                    //remove all previous selection
                    ClearSelection();
                    //updates the board
                    UpdateBoardSelection();
                }
            }
        }

        //quit game
        private void QuitButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {

        }
    }
}
