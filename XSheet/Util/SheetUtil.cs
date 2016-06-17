using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Util
{
    class SheetUtil
    {
        public static Worksheet getSheetByName(String name,WorksheetCollection sheets)
        {
            Worksheet sheet = null;
            try
            {
                sheet = sheets[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Sheet："+name+"不存在!请检查配置！");
            }
            return sheet;
        }

        public static DefinedName getNameinNames(String str, DefinedNameCollection names)
        {
            DefinedName name = null;
            try
            {
                name= names.GetDefinedName(str);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("未在配置Sheet找到名称为" + str + "的配置项，请确认配置");

            }
            return name;
        }
    }
}
