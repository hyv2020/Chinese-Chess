﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public static class FilePaths
    {
        //set the root location of chess piece images
        public static string rootChessImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\chess piece images\");
        //set the root location of board images
        public static string rootBoardImageFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, @"Images\board images\");

    }
}
