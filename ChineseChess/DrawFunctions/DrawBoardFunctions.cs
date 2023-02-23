using System.Drawing;
using System.Windows.Forms;
using GameCommons;

namespace ChineseChess
{
    public static class DrawBoardFunctions
    {
        static string rootBoardImageFilePath = FilePaths.rootBoardImageFilePath;
        static int xOffset = GlobalVariables.XOffset;
        static int yOffset = GlobalVariables.YOffset;
        static int cellSize = GlobalVariables.CellSize;
        static int boardSizeX = GlobalVariables.BoardSizeX;
        static int boardSizeY = GlobalVariables.BoardSizeY;
        static int indicatorToCell = GlobalVariables.IndicatorToCell;
        static int legalMoveBoxSize = GlobalVariables.LegalMoveBoxSize;

        public static PictureBox DrawBoard(int x, int y)
        {

            //show each cell as picturebox
            //make new picturebox call chessCell
            PictureBox boardCell = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                Name = "boardTile" + x + y,
                //size of the box
                Size = new Size(cellSize, cellSize),
                //colour of the box
                BackColor = Color.Blue,
                //the location changes to make the whole board
                Location = new Point(xOffset + x * (cellSize),
                yOffset + y * (cellSize)),
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                //show the box
                Visible = true,
            };
            //draw the board with pictureBox tiles
            //draw the top edge and adviser area
            DrawTopEdge(boardCell, x, y);
            //draw the bottom edge and adviser area
            DrawBottomEdge(boardCell, x, y);
            //draw the sides
            DrawSideEdge(boardCell, x, y);
            //draw the river
            DrawRiver(boardCell, x, y);
            //fill the rest of the board with tiles
            DrawCentreTile(boardCell);

            return boardCell;
        }
        private static void DrawTopEdge(PictureBox chessCell, int x, int y)
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

        private static void DrawBottomEdge(PictureBox chessCell, int x, int y)
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

        private static void DrawSideEdge(PictureBox chessCell, int x, int y)
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

        private static void DrawRiver(PictureBox chessCell, int x, int y)
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

        private static void DrawCentreTile(PictureBox chessCell)
        {
            //fill empty tile with centre tile
            if (chessCell.Image == null)
            {
                chessCell.Image = Image.FromFile(rootBoardImageFilePath + "chinese tile.gif");
            }
        }
        public static PictureBox DrawLegalMoveIndictor(int x, int y)
        {
            PictureBox legalMoveIndicator = new PictureBox
            {
                //properties of picturebox
                //change name of the picturebox
                //use colour and type of the piece
                //for example "red horse"
                Name = "legalMoveIndicator" + x + y,
                //size of the legal move box
                Size = new Size(legalMoveBoxSize, legalMoveBoxSize),
                //colour of the box
                BackColor = Color.Black,
                //border style
                BorderStyle = BorderStyle.None,
                //stretch image to fit in box
                SizeMode = PictureBoxSizeMode.StretchImage,
                //location
                Location = new Point(xOffset + indicatorToCell + x * (cellSize),
                        yOffset + indicatorToCell + y * (cellSize)),
                //hide the box
                Visible = false
            };
            legalMoveIndicator.BringToFront();
            return legalMoveIndicator;
        }

    }
}
