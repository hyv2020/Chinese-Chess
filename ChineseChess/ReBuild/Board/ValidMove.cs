using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ChineseChess
{
    public class ValidMove
    {
        private bool isValid = false;
        public bool Valid 
        { 
            get { return this.isValid; } 
        }
        public PictureBox ValidMovePicBox = new PictureBox();

        public ValidMove(int x, int y)
        {
            isValid = false;
            ValidMovePicBox = DrawBoardFunctions.DrawLegalMoveIndictor(x, y);
        }
        public void IsValidMove()
        {
            isValid = true;
            ValidMovePicBox.Visible = true;
        }
        public void NotValidMove()
        {
            isValid = false;
            ValidMovePicBox.Visible = false;
        }
    }
}
