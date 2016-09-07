﻿using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.CfgBean;
using XSheet.Util;

namespace XSheet.v2.Data.XSheetRange
{
    /// <summary>
    /// Named工厂类，工厂模式+反射，创建不同的Name
    /// </summary>
    public class XRangeFactory
    {
        public static XRange getXRange(DataCfg cfg)
        {
            XRange named = null;
            String nametype = cfg.RangeName.Split('_')[0];
            //XNamedTable
            try
            {
                nametype = "XSheet.v2.Data.XSheetRange.XRange" + nametype.ToUpper();
                Console.WriteLine(nametype);
                Type type = Type.GetType(nametype, true);
                named = (XRange)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("DATA："+cfg.DataName+"无法识别类型："+nametype);
            }
            return named;
        }
    }
}
