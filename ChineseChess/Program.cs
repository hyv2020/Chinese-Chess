using System;
using System.Windows.Forms;

namespace ChineseChess
{
    public static class Program
    {
        public static MainMenu mainMenu;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainMenu = new MainMenu());
        }
    }
}
