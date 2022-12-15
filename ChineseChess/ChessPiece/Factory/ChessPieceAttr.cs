using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    internal class ChessPieceAttr:Attribute
    {
        public ChessPieceType Type { get; set; }
        public ChessPieceAttr(ChessPieceType type)
        {
            Type = type;
        }
    }
}
