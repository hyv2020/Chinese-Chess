namespace ChineseChess
{
    public class ChessPieceProperties
    {
        //define piece type
        public enum ChessPieceType
        {
            Soldier,
            Horse,
            Minister,
            Advisor,
            Cannon,
            Chariot,
            General
        }

        //create side properties
        public enum SideColour
        {
            Red,
            Black,
        }

        //name to identify object and pair it with pictureBox
        //it is unique so nothing overlaps
        public string name { get; set; }
        //piece location x
        public int PieceX { get; set; }
        //piece location y
        public int PieceY { get; set; }

        //define side colour
        public ChessPieceProperties.SideColour PieceColour { get; set; }
        //define piece type
        public ChessPieceProperties.ChessPieceType PieceType { get; set; }
        //the river marks each side. check which side of the river the piece is on
        public bool CrossRiver { get; set; }

        //check selection
        public bool IsSelected { get; set; }

        //only move if its your turn
        public bool WasMoved { get; set; }

        //occupies space

        //get destroyed if taken

        //constructor
        public ChessPieceProperties()
        {
            //has piece type
            //has side colour         
        }
        //different types of chess pieces are sub-class of ChessPiece
        //they share basic properties but has unique movement rules
    }
}
