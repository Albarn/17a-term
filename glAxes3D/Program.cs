using System;
using System.Windows.Forms;

namespace glAxes3D
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] cmd)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (cmd.Length == 0) cmd = new string[] { "/s" };
            switch (cmd[0])
            {

                //настройка
                case "/c":
                    Application.Run(new SettingsForm());
                    ; break;
                //на весь экран
                case "/s":
                    Application.Run(new MainForm());
                    ;break;
                //предварительный просмотр
                case "/p":
                    {
                        MainForm f = new MainForm();
                        f.WindowState = FormWindowState.Normal;
                        f.FormBorderStyle = FormBorderStyle.FixedDialog;
                        f.cursorHide = false;
                        Application.Run(f);
                    };break;
            }
        }
    }
}
