using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data.Common;
using XSheet.Util;
using System.Windows.Forms;
using XSheet.v2.Data;
using XSheet.v2.CfgBean;

namespace XSheet.Data.Action
{
    public class ActionSQL : XAction
    {
        public override string doAction()
        {
            Range range = dRange.getRange();
            for (int row = 1; row < range.RowCount; row++)
            {
                //Range range = action.dRange.getRange();
                setSelectIndex(range.TopRowIndex + row);
                range.Worksheet.Workbook.Calculate();
                String Sql = getRealStatement();
                Console.WriteLine(Sql);
                try
                {
                    DbDataAdapter da = dRange.getDbDataAdapter(Sql);
                    da.SelectCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("SQL:" + Sql + "\n" + e.ToString());
                }

                //dt.Rows[row].Delete();
            }
            return "OK";
        }
    }
}
