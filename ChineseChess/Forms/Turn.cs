using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class Turn
    {
        public readonly int TurnNumber;
        public readonly Side WhosTurn;
        public readonly List<string> BoardState;
        public Turn(int turnNumber, Side whosTurn, List<string> boardState)
        {
            TurnNumber = turnNumber;
            WhosTurn = whosTurn;
            BoardState = boardState;
        }
    }
}
