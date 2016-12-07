using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using XSheet.v2.Form;
using System.IO;
using XSheet.v2.Util;
using XSheet.v2.Privilege;

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
            StreamWriter sw = new StreamWriter(date.ToLongDateString()+".txt",true);
            TextWriter temp = Console.Out;
            Console.SetOut(sw);
            date = DateTime.Now;
            Console.WriteLine("start:"+date.ToString());
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            XSheetUser user = new XSheetUser(System.Environment.UserDomainName, System.Environment.UserName, System.Environment.MachineName, System.Environment.OSVersion.ToString());
            if (args.Length == 0)
            {
                try
                {
                    Application.Run(new MainForm(user));
                    //Application.Run(new XSheetMain());
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
            }
            else
            {
                String param = args[0];
                Application.Run(new XSheetMain(args[0],user));
                //Application.Run(new XSheetMain());
            }
            
        }
    }
}