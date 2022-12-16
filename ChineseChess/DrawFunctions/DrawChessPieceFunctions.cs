using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess
{
    public static class DrawChessPieceFunctions
    {
        static int chessSize = GlobalVariables.ChessSize;
        static int xOffset = GlobalVariables.XOffset;
        static int yOffset = GlobalVariables.YOffset;
        static int cellSize = GlobalVariables.CellSize;
        private static int chessToCell = GlobalVariables.ChessToCell;
        public static PictureBox DrawChessPiece(ChessPiece chessPiece)
        {
            //ideally it could use the location of the cell of the board to snap to the grid
            //there is a loop to determine its location and draw
            //the loop look through ChessPieceList to find out how many it needs to draw
            //the location is set depending on colour and piece type

            //show each cell as picturebox
            //make new picturebox call chessCell
            PictureBox chessPictureBox = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                //use colour and type of the piece
                //for example "red horse"
                Name = chessPiece.Name,
                //size of the chess piece box
                Size = new Size(chessSize, chessSize),
                //colour of the box
                BackColor = Color.White,
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Image.FromFile(FilePaths.rootChessImageFilePath + $"{chessPiece.Side}{chessPiece.GetChessPieceType()}.gif"),
                Location = new Point(xOffset + chessToCell + chessPiece.X * cellSize, yOffset + chessToCell + chessPiece.Y * cellSize),
                //show the box
                Visible = true

            };
            chessPictureBox.BringToFront();
            return chessPictureBox;
        }

    }
}
