using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using XSheet.v2.Form;
using System.IO;
using XSheet.v2.Util;

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
            DateTime date = new DateTime();
            StreamWriter sw = new StreamWriter(@"ConsoleOutput.txt",true);
            TextWriter temp = Console.Out;
            Console.SetOut(sw);
            date = DateTime.Now;
            Console.WriteLine("start:"+date.ToString());
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            if (args.Length == 0)
            {
                try
                {
                    date = DateTime.Now;
                    Console.WriteLine("before:" + date.ToString());
                    sw.Flush();
                    sw.Close();
                    Console.SetOut(temp);
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