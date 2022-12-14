using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess
{
    [ChessPieceAttr(ChessPieceType.Soldier)]
    public class Soldierv2: ChessPiece
    {
        public Soldierv2(int x, int y, Side side, ChessBoard chessBoard): base(x,y,side, chessBoard)
        {

        }
        public override List<Point> FindValidMove(ChessBoard chessBoard)
        {
            throw new NotImplementedException();
        }
    }
}
