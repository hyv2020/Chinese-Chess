namespace ChineseChess
{

    //this class has the properties of the chess board
    public class Board
    {
        //size of the board
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        //2d array of the grid aka the board
        public Cell[,] TheGrid { get; set; }
        //public ChessPieceProperties[,] piecePosition { get; set; } 

        //constructor
        public Board(int sx, int sy)
        {
            //initial size of the board is defined by SizeX and SizeY
            //initial width of the board
            SizeX = sx;
            //initial length of the board
            SizeY = sy;
            TheGrid = new Cell[SizeX, SizeY];
            //make the grid and map the cells
            //fill the 2d array with cells with unqiue x and y coordinates

            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    //make the cells
                    TheGrid[i, j] = new Cell(i, j);
                }
            }
        }
    }
}

