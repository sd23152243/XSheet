using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Util
{
    public static class LogWriter
    {
        public static void log(String text)
        {
            DateTime date = DateTime.Now;
            StreamWriter sw = new StreamWriter("err_"+DateTime.Today.ToString("yyyyMMdd") + ".log", true);

            sw.WriteLine("============================================================================");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":");
            sw.WriteLine(text);
            sw.Close();
        }
    }
}
