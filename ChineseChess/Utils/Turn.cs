using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ChineseChess
{
    [Serializable]
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
        public Turn()
        {

        }
        public void SaveToFile()
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
        public byte[] ToByteArray()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, this);
                return memoryStream.ToArray();
            }
        }
        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="byteArray">byte array to get object</param>
        /// <returns>object</returns>
        public static Turn ByteArrayToObject(byte[] byteArray)
        {
            try
            {
                // convert byte array to memory stream
                MemoryStream memoryStream = new MemoryStream(byteArray);

                // create new BinaryFormatter
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                // set memory stream position to starting point
                memoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                return binaryFormatter.Deserialize(memoryStream) as Turn;
            }
            catch (Exception exception)
            {
                // Error
                Debug.WriteLine("Exception caught in process: {0}", exception.ToString());
            }

            // Error occured, return null
            return null;
        }
    }
}
