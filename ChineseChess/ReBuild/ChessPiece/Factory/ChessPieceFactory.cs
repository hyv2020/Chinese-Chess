using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChineseChess
{
    public static class ChessPieceFactory
    {
        public static ChessPiece CreateChessPiece(int x, int y, Side side, ChessPieceType chessPieceType, ChessBoard chessBoard )
        {
            switch (chessPieceType)
            {
                case ChessPieceType.Soldier:
                    return CreateSoldier(side, x, y, chessBoard);
                case ChessPieceType.Horse:
                    return CreateHorse(side, x, y, chessBoard);
                case ChessPieceType.Cannon:
                    return CreateCannon(side, x, y, chessBoard);
                case ChessPieceType.Chariot:
                    return CreateChariot(side, x, y, chessBoard);
                case ChessPieceType.Minister:
                    return CreateMinister(side, x, y, chessBoard);
                case ChessPieceType.Advisor:
                    return CreateAdvisor(side, x, y, chessBoard);
                case ChessPieceType.General:
                    return CreateGeneral(side, x, y, chessBoard);
                default:
                    throw new ArgumentOutOfRangeException(nameof(chessPieceType));
            }
        }
        private static ChessPiece CreateSoldier(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Soldierv2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateHorse(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Horsev2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateCannon(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Cannonv2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateChariot(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Chariotv2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateMinister(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Ministerv2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateAdvisor(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Advisorv2(x, y, side, chessBoard);
        }
        private static ChessPiece CreateGeneral(Side side, int x, int y, ChessBoard chessBoard)
        {
            return new Generalv2(x, y, side, chessBoard);
        }
    }
}
