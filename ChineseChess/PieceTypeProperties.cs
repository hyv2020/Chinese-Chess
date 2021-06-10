using System.Collections.Generic;

namespace ChineseChess
{
    //set specific rules for each type of chess piece

    //different types of chess pieces are sub-class of ChessPiece
    //they share basic properties but has unique rules
    public class Soldier : ChessPieceProperties
    {
        //a function to make new soldier pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and soldiers information
        public void MakeSoldierPiece(int numberOfSoldiers, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Soldier> soldierList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfSoldiers; i++)
            {
                //red side
                Soldier p1ChessPiece = new Soldier()
                {
                    name = "redSoldier" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Soldier,
                    //define the its location using the board in MakeBoard
                    PieceX = i * 2,
                    PieceY = boardSizeY - 4,
                };
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to the soldier type
                soldierList.Add(p1ChessPiece);

                //black side
                Soldier p2ChessPiece = new Soldier()
                {
                    name = "blackSoldier" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Soldier,
                    //define the its location using the board in MakeBoard
                    PieceX = i * 2,
                    PieceY = 3,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to the soldier list
                soldierList.Add(p2ChessPiece);
            }
        }

        //show soldier legal move if it is selected
        //add board into parameter to use the grid to find legal moves
        //this function defined how the piece moves
        public void GetSoldierValidMove(Board chessBoard)
        {
            //soldiers can only go forward one cell
            //it can go side ways one cell if it crosses the river
            //make sure the move can not go out of bound

            //check selection
            if (IsSelected == true)
            {
                SideColour colourSelected = PieceColour;
                if (PieceX - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY].LegalNextMove = true;
                }
                if (PieceY - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX, PieceY - 1].LegalNextMove = true;
                }
                if (PieceY + 1 < chessBoard.SizeY)
                {
                    chessBoard.TheGrid[PieceX, PieceY + 1].LegalNextMove = true;
                }
                if (colourSelected == SideColour.Red && PieceY + 1 < chessBoard.SizeY)
                {
                    chessBoard.TheGrid[PieceX, PieceY + 1].LegalNextMove = false;
                }
                else if (colourSelected == SideColour.Black && PieceY - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX, PieceY - 1].LegalNextMove = false;

                }
                if (CrossRiver == true && PieceX - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY].LegalNextMove = false;
                }
                if (CrossRiver == true && PieceX + 1 < chessBoard.SizeX)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY].LegalNextMove = false;

                }
            }
        }
        //constructor
        public Soldier()
        {

        }
    }


    public class Horse : ChessPieceProperties
    {
        //a function to make new horse pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and horses information
        public void MakeHorsePiece(int numberOfHorses, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Horse> horseList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfHorses; i++)
            {
                //red side
                Horse p1ChessPiece = new Horse
                {
                    name = "redHorse" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Horse,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 3) + 1,
                    PieceY = boardSizeY - 1,
                };
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to horse list
                horseList.Add(p1ChessPiece);

                //black side
                Horse p2ChessPiece = new Horse
                {
                    name = "blackHorse" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Horse,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 3) + 1,
                    PieceY = 0,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to horse list
                horseList.Add(p2ChessPiece);
            }
        }

        //moves like knight
        //rules for the horse, if it is blocked, it can not move
        //if statement is the "hobbling the horse's leg" rule
        public void GetHorseValidMove(Board chessBoard)
        {
            if (PieceX - 2 >= 0)
            {
                if (PieceY - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX - 2, PieceY - 1].LegalNextMove = true;
                }
                if (PieceY + 1 < chessBoard.SizeY)
                {
                    chessBoard.TheGrid[PieceX - 2, PieceY + 1].LegalNextMove = true;
                }
                if (PieceX - 1 >= 0)
                {
                    if (chessBoard.TheGrid[PieceX - 1, PieceY].CurrentlyOccupied == true)
                    {
                        if (PieceY + 1 < chessBoard.SizeY)
                        {
                            chessBoard.TheGrid[PieceX - 2, PieceY + 1].LegalNextMove = false;

                        }
                        if (PieceY - 1 >= 0)
                        {
                            chessBoard.TheGrid[PieceX - 2, PieceY - 1].LegalNextMove = false;
                        }
                    }
                }
            }
            if (PieceX + 2 < chessBoard.SizeX)
            {
                if (PieceY - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX + 2, PieceY - 1].LegalNextMove = true;
                }
                if (PieceY + 1 < chessBoard.SizeY)
                {
                    chessBoard.TheGrid[PieceX + 2, PieceY + 1].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX)
                {
                    if (chessBoard.TheGrid[PieceX + 1, PieceY].CurrentlyOccupied == true)
                    {
                        if (PieceY - 1 >= 0)
                        {
                            chessBoard.TheGrid[PieceX + 2, PieceY - 1].LegalNextMove = false;
                        }
                        if (PieceY + 1 < chessBoard.SizeY)
                        {
                            chessBoard.TheGrid[PieceX + 2, PieceY + 1].LegalNextMove = false;
                        }
                    }
                }

            }
            if (PieceY - 2 >= 0)
            {
                if (PieceX - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY - 2].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY - 2].LegalNextMove = true;
                }
                if (PieceY - 1 >= 0)
                {
                    if (chessBoard.TheGrid[PieceX, PieceY - 1].CurrentlyOccupied == true)
                    {
                        if (PieceX - 1 >= 0)
                        {
                            chessBoard.TheGrid[PieceX - 1, PieceY - 2].LegalNextMove = false;
                        }
                        if (PieceX + 1 < chessBoard.SizeX)
                        {
                            chessBoard.TheGrid[PieceX + 1, PieceY - 2].LegalNextMove = false;
                        }
                    }
                }
            }
            if (PieceY + 2 < chessBoard.SizeY)
            {
                if (PieceX - 1 >= 0)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY + 2].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY + 2].LegalNextMove = true;
                }
                if (PieceY + 1 < chessBoard.SizeY)
                {
                    if (chessBoard.TheGrid[PieceX, PieceY + 1].CurrentlyOccupied == true)
                    {
                        if (PieceX - 1 >= 0)
                        {
                            chessBoard.TheGrid[PieceX - 1, PieceY + 2].LegalNextMove = false;
                        }
                        if (PieceX + 1 < chessBoard.SizeX)
                        {
                            chessBoard.TheGrid[PieceX + 1, PieceY + 2].LegalNextMove = false;
                        }
                    }
                }
            }
        }

        //constructor
        public Horse()
        {

        }
    }
    public class Minister : ChessPieceProperties
    {
        //a function to make new minister pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and ministers information
        public void MakeMinisterPiece(int numberOfMinisters, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Minister> ministerList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfMinisters; i++)
            {
                //red side
                Minister p1ChessPiece = new Minister
                {
                    name = "redMinister" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Minister,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 5) + 2,
                    PieceY = boardSizeY - 1,
                };
                //add it to the board
                chessBoard.TheGrid[p1ChessPiece.PieceX, p1ChessPiece.PieceY].CurrentlyOccupied = true;
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to minister list
                ministerList.Add(p1ChessPiece);

                //black side
                Minister p2ChessPiece = new Minister
                {
                    name = "blackminister" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Minister,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 5) + 2,
                    PieceY = 0,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to minister list
                ministerList.Add(p2ChessPiece);
            }
        }

        //minister go diagonal 2 cells
        //but it can not cross the river
        //if it is blocked, it can not move
        //if statement makes any move across the river illegal
        public void GetMinisterValidMove(Board chessBoard)
        {
            SideColour colourSelected = PieceColour;
            if (IsSelected == true)
            {
                if (colourSelected == SideColour.Red)
                {
                    if (PieceX - 2 >= 0 && PieceY - 2 >= 5)
                    {
                        chessBoard.TheGrid[PieceX - 2, PieceY - 2].LegalNextMove = true;
                    }
                    if (PieceX - 2 >= 0 && PieceY + 2 < chessBoard.SizeY)
                    {
                        chessBoard.TheGrid[PieceX - 2, PieceY + 2].LegalNextMove = true;
                    }
                    if (PieceX + 2 < chessBoard.SizeX && PieceY - 2 >= 5)
                    {
                        chessBoard.TheGrid[PieceX + 2, PieceY - 2].LegalNextMove = true;
                    }
                    if (PieceX + 2 < chessBoard.SizeX && PieceY + 2 < chessBoard.SizeY)
                    {
                        chessBoard.TheGrid[PieceX + 2, PieceY + 2].LegalNextMove = true;
                    }
                }
                else if (colourSelected == SideColour.Black)
                {
                    if (PieceX - 2 >= 0 && PieceY - 2 >= 0)
                    {
                        chessBoard.TheGrid[PieceX - 2, PieceY - 2].LegalNextMove = true;
                    }
                    if (PieceX - 2 >= 0 && PieceY + 2 < 6)
                    {
                        chessBoard.TheGrid[PieceX - 2, PieceY + 2].LegalNextMove = true;
                    }
                    if (PieceX + 2 < chessBoard.SizeX && PieceY - 2 >= 0)
                    {
                        chessBoard.TheGrid[PieceX + 2, PieceY - 2].LegalNextMove = true;
                    }
                    if (PieceX + 2 < chessBoard.SizeX && PieceY + 2 < 6)
                    {
                        chessBoard.TheGrid[PieceX + 2, PieceY + 2].LegalNextMove = true;
                    }
                }
            }
        }
    }
    public class Advisor : ChessPieceProperties
    {
        //a function to make new adisor pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and advisor information
        public void MakeAdvisorPiece(int numberOfAdvisors, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Advisor> advisorList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfAdvisors; i++)
            {
                //red side
                Advisor p1ChessPiece = new Advisor
                {
                    name = "redAdvisor" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Advisor,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 7) + 3,
                    PieceY = boardSizeY - 1,
                };
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to advisor list
                advisorList.Add(p1ChessPiece);

                //black side
                Advisor p2ChessPiece = new Advisor
                {
                    name = "blackAdvisor" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Advisor,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 7) + 3,
                    PieceY = 0,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to advisor list
                advisorList.Add(p2ChessPiece);
            }
        }

        //it moves diagonal one cell
        //can not move outside of its area
        //the if statement make every move outside of area illegal
        public void GetAdvisorValidMove(Board chessBoard)
        {
            if (IsSelected == true)
            {
                if (PieceX - 1 >= 0 && PieceY - 1 >= 0 && chessBoard.TheGrid[PieceX - 1, PieceY - 1].AdviserArea == true)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY - 1].LegalNextMove = true;
                }
                if (PieceX - 1 >= 0 && PieceY + 1 < chessBoard.SizeY && chessBoard.TheGrid[PieceX - 1, PieceY + 1].AdviserArea == true)
                {
                    chessBoard.TheGrid[PieceX - 1, PieceY + 1].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX && PieceY - 1 >= 0 && chessBoard.TheGrid[PieceX + 1, PieceY - 1].AdviserArea == true)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY - 1].LegalNextMove = true;
                }
                if (PieceX + 1 < chessBoard.SizeX && PieceY + 1 < chessBoard.SizeY && chessBoard.TheGrid[PieceX + 1, PieceY + 1].AdviserArea == true)
                {
                    chessBoard.TheGrid[PieceX + 1, PieceY + 1].LegalNextMove = true;
                }
            }
        }
    }
    public class Cannon : ChessPieceProperties
    {
        //a function to make new cannon pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and cannons information
        public void MakeCannonPiece(int numberOfCannons, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Cannon> cannonList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfCannons; i++)
            {
                //red side
                Cannon p1ChessPiece = new Cannon
                {
                    name = "redCannon" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Cannon,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 3) + 1,
                    PieceY = boardSizeY - 3,
                };
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to cannon list
                cannonList.Add(p1ChessPiece);

                //black side
                Cannon p2ChessPiece = new Cannon
                {
                    name = "blackCannon" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Cannon,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 3) + 1,
                    PieceY = 2,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to cannon list
                cannonList.Add(p2ChessPiece);
            }
        }

        //moves like rook but can not kill piece directly
        //need a piece in between to take the piece behind it
        //the kill function is in Game file because it needs to know it is selected to kill
        public void GetCannonValidMove(Board chessBoard)
        {
            //x axis moves
            //scan the whole axis
            for (int x = 0; x < chessBoard.SizeX; x++)
            {
                //make the whole axis legal
                //take the illegal cells away later
                chessBoard.TheGrid[x, PieceY].LegalNextMove = true;
            }
            //if its already occupied
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceXSmaller = 0;
            int blockingPieceXGreater = 0;
            //going right
            int occupiedRightCounter = 0;

            for (int i = PieceX + 1; i < chessBoard.SizeX; i++)
            {
                if (blockingPieceXGreater == 0 && chessBoard.TheGrid[i, PieceY].CurrentlyOccupied == true)
                {
                    blockingPieceXGreater = i;
                    occupiedRightCounter += 1;
                    chessBoard.TheGrid[i, PieceY].LegalNextMove = false;
                }
                else if (occupiedRightCounter > 0 && blockingPieceXGreater != 0 && i > blockingPieceXGreater)
                {
                    chessBoard.TheGrid[i, PieceY].LegalNextMove = false;
                }

            }

            //if its already occupied
            //the cells behind them are not legal
            //going left
            int occupiedLeftCounter = 0;

            for (int j = PieceX - 1; j >= 0; j--)
            {
                if (blockingPieceXSmaller == 0 && chessBoard.TheGrid[j, PieceY].CurrentlyOccupied == true)
                {
                    blockingPieceXSmaller = j;
                    occupiedLeftCounter += 1;
                    chessBoard.TheGrid[j, PieceY].LegalNextMove = false;
                }
                if (occupiedLeftCounter > 0 && blockingPieceXSmaller != 0 && j < blockingPieceXSmaller)
                {
                    chessBoard.TheGrid[j, PieceY].LegalNextMove = false;
                }
            }
            //for the y axis
            for (int y = 0; y < chessBoard.SizeY; y++)
            {
                //make the whole axis legal
                //take them illegal cells away later
                chessBoard.TheGrid[PieceX, y].LegalNextMove = true;
            }
            //if its already occupied
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceYSmaller = 0;
            int blockingPieceYGreater = 0;
            //going down
            int occupiedDownCounter = 0;

            for (int i = PieceY + 1; i < chessBoard.SizeY; i++)
            {
                if (blockingPieceYGreater == 0 && chessBoard.TheGrid[PieceX, i].CurrentlyOccupied == true)
                {
                    blockingPieceYGreater = i;
                    occupiedDownCounter += 1;
                    chessBoard.TheGrid[PieceX, i].LegalNextMove = false;
                }
                else if (occupiedDownCounter > 0 && blockingPieceYGreater != 0 && i > blockingPieceYGreater)
                {
                    chessBoard.TheGrid[PieceX, i].LegalNextMove = false;
                }

            }

            //if its already occupied
            //the cells behind them are not legal
            //going up
            int occupiedUpCounter = 0;

            for (int j = PieceY - 1; j >= 0; j--)
            {
                if (blockingPieceYSmaller == 0 && chessBoard.TheGrid[PieceX, j].CurrentlyOccupied == true)
                {
                    blockingPieceYSmaller = j;
                    occupiedUpCounter += 1;
                    chessBoard.TheGrid[PieceX, j].LegalNextMove = false;
                }
                if (occupiedUpCounter > 0 && blockingPieceYSmaller != 0 && j < blockingPieceYSmaller)
                {
                    chessBoard.TheGrid[PieceX, j].LegalNextMove = false;
                }
            }
            //current cell is not a legal move
            chessBoard.TheGrid[PieceX, PieceY].LegalNextMove = false;
        }
    }

    public class Chariot : ChessPieceProperties
    {
        //a function to make new chariot pieces
        //it uses variables from Games.cs
        //it need the number of the piece, the board size, the board object
        //the list to store pieces and chariots information
        public void MakeChariotPiece(int numberOfChariots, int boardSizeX, int boardSizeY,
            Board chessBoard, List<Chariot> chariotList, List<ChessPieceProperties> chessPieceList)
        {
            //use loop to make more chess pieces
            //make the piece for game
            for (int i = 0; i < numberOfChariots; i++)
            {
                //red side
                Chariot p1ChessPiece = new Chariot
                {
                    name = "redChariot" + i,
                    //define the side red of the piece
                    PieceColour = ChessPieceProperties.SideColour.Red,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Chariot,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 1),
                    PieceY = boardSizeY - 1,
                };
                //add to the list of p1 pieces
                chessPieceList.Add(p1ChessPiece);
                //add to chariot list
                chariotList.Add(p1ChessPiece);

                //black side
                Chariot p2ChessPiece = new Chariot
                {
                    name = "blackChariot" + i,
                    //define the side black of the piece
                    PieceColour = ChessPieceProperties.SideColour.Black,
                    //define the type of the piece
                    PieceType = ChessPieceProperties.ChessPieceType.Chariot,
                    //define the its location using the board in MakeBoard
                    PieceX = i * (boardSizeX - 1),
                    PieceY = 0,
                };
                //add to the list of p2 pieces
                chessPieceList.Add(p2ChessPiece);
                //add to chariot list
                chariotList.Add(p2ChessPiece);
            }
        }

        //moves like rook
        public void GetChariotMove(Board chessBoard)
        {
            //x axis moves
            //scan the whole axis
            for (int x = 0; x < chessBoard.SizeX; x++)
            {
                //make the whole axis legal
                //take the illegal cells away later
                chessBoard.TheGrid[x, PieceY].LegalNextMove = true;
            }
            //if its already occupied
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceXSmaller = 0;
            int blockingPieceXGreater = 0;
            //going right
            int occupiedRightCounter = 0;

            for (int i = PieceX + 1; i < chessBoard.SizeX; i++)
            {
                if (blockingPieceXGreater == 0 && chessBoard.TheGrid[i, PieceY].CurrentlyOccupied == true)
                {
                    blockingPieceXGreater = i;
                    occupiedRightCounter += 1;
                }
                else if (occupiedRightCounter > 0 && blockingPieceXGreater != 0 && i > blockingPieceXGreater)
                {
                    chessBoard.TheGrid[i, PieceY].LegalNextMove = false;
                }
            }

            //if its already occupied
            //the cells behind them are not legal
            //going left
            int occupiedLeftCounter = 0;

            for (int j = PieceX - 1; j >= 0; j--)
            {
                if (blockingPieceXSmaller == 0 && chessBoard.TheGrid[j, PieceY].CurrentlyOccupied == true)
                {
                    blockingPieceXSmaller = j;
                    occupiedLeftCounter += 1;
                }
                if (occupiedLeftCounter > 0 && blockingPieceXSmaller != 0 && j < blockingPieceXSmaller)
                {
                    chessBoard.TheGrid[j, PieceY].LegalNextMove = false;
                }
            }
            //for the y axis
            for (int y = 0; y < chessBoard.SizeY; y++)
            {
                //make the whole axis legal
                //take them illegal cells away later
                chessBoard.TheGrid[PieceX, y].LegalNextMove = true;
            }
            //if its already occupied
            //the cells behind them are not legal
            //set variables for the first blocking piece
            int blockingPieceYSmaller = 0;
            int blockingPieceYGreater = 0;
            //going down
            int occupiedDownCounter = 0;

            for (int i = PieceY + 1; i < chessBoard.SizeY; i++)
            {
                if (blockingPieceYGreater == 0 && chessBoard.TheGrid[PieceX, i].CurrentlyOccupied == true)
                {
                    blockingPieceYGreater = i;
                    occupiedDownCounter += 1;
                }
                else if (occupiedDownCounter > 0 && blockingPieceYGreater != 0 && i > blockingPieceYGreater)
                {
                    chessBoard.TheGrid[PieceX, i].LegalNextMove = false;
                }
            }

            //if its already occupied
            //the cells behind them are not legal
            //going up
            int occupiedUpCounter = 0;

            for (int j = PieceY - 1; j >= 0; j--)
            {
                if (blockingPieceYSmaller == 0 && chessBoard.TheGrid[PieceX, j].CurrentlyOccupied == true)
                {
                    blockingPieceYSmaller = j;
                    occupiedUpCounter += 1;
                }
                if (occupiedUpCounter > 0 && blockingPieceYSmaller != 0 && j < blockingPieceYSmaller)
                {
                    chessBoard.TheGrid[PieceX, j].LegalNextMove = false;
                }
            }
            //current cell is not a legal move
            chessBoard.TheGrid[PieceX, PieceY].LegalNextMove = false;
        }
    }
    public class General : ChessPieceProperties
    {
        //a function to make new general pieces
        //it uses variables from Games.cs
        //it need the board size, the board object
        //the list to store pieces and general information
        public void MakeGeneralPiece(int boardSizeX, int boardSizeY,
            Board chessBoard, List<General> generalList, List<ChessPieceProperties> chessPieceList)
        {
            //only one general so no loop
            //make the piece for game
            //red side
            General p1ChessPiece = new General
            {
                name = "redGeneral",
                //define the side red of the piece
                PieceColour = ChessPieceProperties.SideColour.Red,
                //define the type of the piece
                PieceType = ChessPieceProperties.ChessPieceType.General,
                //define the its location using the board in MakeBoard
                PieceX = 4,
                PieceY = boardSizeY - 1,
            };
            //add to the list of p1 pieces
            chessPieceList.Add(p1ChessPiece);
            //add to general list
            generalList.Add(p1ChessPiece);

            //black side
            General p2ChessPiece = new General
            {
                name = "blackGeneral",
                //define the side black of the piece
                PieceColour = ChessPieceProperties.SideColour.Black,
                //define the type of the piece
                PieceType = ChessPieceProperties.ChessPieceType.General,
                //define the its location using the board in MakeBoard
                PieceX = 4,
                PieceY = 0,
            };
            //add to the list of p2 pieces
            chessPieceList.Add(p2ChessPiece);
            //add to general list
            generalList.Add(p2ChessPiece);
        }

        //moves forward, backward, left, right one cell
        //can not move out of area
        //can check the opponent general if no units in between
        public void GetGeneralValidMove(Board chessBoard)
        {
            if (PieceX - 1 >= 0 && chessBoard.TheGrid[PieceX - 1, PieceY].AdviserArea == true)
            {
                chessBoard.TheGrid[PieceX - 1, PieceY].LegalNextMove = true;
            }
            if (PieceY + 1 < chessBoard.SizeY && chessBoard.TheGrid[PieceX, PieceY + 1].AdviserArea == true)
            {
                chessBoard.TheGrid[PieceX, PieceY + 1].LegalNextMove = true;
            }
            if (PieceX + 1 < chessBoard.SizeX && chessBoard.TheGrid[PieceX + 1, PieceY].AdviserArea == true)
            {
                chessBoard.TheGrid[PieceX + 1, PieceY].LegalNextMove = true;
            }
            if (PieceY - 1 >= 0 && chessBoard.TheGrid[PieceX, PieceY - 1].AdviserArea == true)
            {
                chessBoard.TheGrid[PieceX, PieceY - 1].LegalNextMove = true;
            }
        }
    }
}
