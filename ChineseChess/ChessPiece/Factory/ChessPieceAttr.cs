using GameCommons;
using System;

namespace ChineseChess
{
    internal class ChessPieceAttr : Attribute
    {
        public ChessPieceType Type { get; set; }
        public ChessPieceAttr(ChessPieceType type)
        {
            Type = type;
        }
    }
}
