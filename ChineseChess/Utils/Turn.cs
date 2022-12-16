using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
        public void SaveTurnToFile()
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(FilePaths.rootTempFilePath, $"{this.TurnNumber}.txt")))
            {
                outputFile.WriteLine(this.WhosTurn.ToString());
                foreach (string row in BoardState)
                {
                    outputFile.WriteLine(row);
                }
            }
        }
    }
}
