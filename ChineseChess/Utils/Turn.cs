using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

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
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, this);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="_ByteArray">byte array to get object</param>
        /// <returns>object</returns>
        public static Turn ByteArrayToObject(byte[] _ByteArray)
        {
            try
            {
                // convert byte array to memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_ByteArray);

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // set memory stream position to starting point
                _MemoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                var obj = _BinaryFormatter.Deserialize(_MemoryStream);
                return (Turn)obj;
            }
            catch (Exception _Exception)
            {
                // Error
                Debug.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }
    }
}
