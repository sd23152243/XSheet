using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;
using XSheet.Util;

namespace XSheet.Data
{
    /// <summary>
    /// Named工厂类，工厂模式+反射，创建不同的Name
    /// </summary>
    public class XNamedFactory
    {
        public static XNamed getXNamed(RangeCfgData cfg)
        {
            XNamed named = null;
            String nametype = cfg.rangeType;
            nametype = "XSheet.Data.XNamed" + nametype.Substring(0, 1).ToUpper() + nametype.Substring(1).ToLower();
            //XNamedTable
            try
            {
                Type type = Type.GetType(nametype, true);
                named = (XNamed)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("非法类型："+cfg.rangeType);
            }
            return named;
        }
    }
}
