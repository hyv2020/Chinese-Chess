namespace ChineseChess
{
    public class GlobalVariables
    {
        public const int BoardSizeX = 9;
        public const int BoardSizeY = 10;
        public const int XOffset = 25;
        public const int YOffset = 25;
        /// <summary>
        /// size of chess piece
        /// </summary>
        public const int ChessSize = 55;
        //size of each square
        public const int CellSize = 65;
        //size of legal move indicator
        public const int LegalMoveBoxSize = 25;

        //extra padding to make everything snip to grid
        public const int ChessToCell = (CellSize - ChessSize) / 2;
        public const int IndicatorToCell = (CellSize - LegalMoveBoxSize) / 2;
        public const string LoadDialogTitle = "Load Save Files";
    }
}
