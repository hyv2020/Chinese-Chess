using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace ChineseChess
{


    public static class ChessPieceMethods
    {
        
        public static ChessPieceType GetChessPieceType(this ChessPiece chessPiece)
        {
            if(chessPiece != null)
            {
                var chessType = chessPiece.GetType();
                object[] attrs = Attribute.GetCustomAttributes(chessType);
                foreach (object attr in attrs)
                {
                    ChessPieceAttr chessPieceAttr = attr as ChessPieceAttr;
                    if (chessPieceAttr != null)
                    {
                        return chessPieceAttr.Type;
                    }
                }
                throw new Exception();
            }
            else
            {
                return ChessPieceType.Null;
            }
        }
    }
    
}
