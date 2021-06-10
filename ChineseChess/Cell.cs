namespace ChineseChess
{
    //properties of each cell of the chess board
    public class Cell
    {
        //get and set values from chess board console app
        //get row number of chess piece from app
        public int RowNumber { get; set; }
        //get column number of chess piece from app
        public int ColumnNumber { get; set; }
        //check if the cell is occupied by a chess piece
        public bool CurrentlyOccupied { get; set; }
        //check if it is selected
        public bool SelectedCell { get; set; }
        //check if this cell is a legal move for selected chess piece
        public bool LegalNextMove { get; set; }

        //adviser area. check if the piece is inside the area
        public bool AdviserArea { get; set; }

        //constructor
        public Cell(int x, int y)
        {
            RowNumber = x;
            ColumnNumber = y;
        }
    }
}
