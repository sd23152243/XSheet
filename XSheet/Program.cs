using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using XSheet.v2.Form;
using System.IO;

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
                try
                {
                    Application.Run(new XSheetDesigner());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else
            {
                String param = args[0];

                Application.Run(new XSheetDesigner(args[0]));
                //Application.Run(new XSheetMain());
            }
            
        }
    }
}