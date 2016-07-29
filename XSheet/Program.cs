using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;

namespace XSheet
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            if (args.Length == 0)
            {
                //Application.Run(new XSheetDesigner());
                Application.Run(new XSheetMain());
            }
            else
            {
                Application.Run(new XSheetMain(args[0]));
            }
            
        }
    }
}