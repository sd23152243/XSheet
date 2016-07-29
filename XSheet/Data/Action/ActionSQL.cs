using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data.Common;
using XSheet.Util;
using System.Windows.Forms;

namespace XSheet.Data.Action
{
    class ActionSQL : XAction
    {
        public override string doAction()
        {
            Range range = dRange.getRange();
            String type = cfg.actionParam;
            for (int row = 1; row < range.RowCount; row++)
            {
                //Range range = action.dRange.getRange();
                setSelectIndex(range.TopRowIndex + row);
                range.Worksheet.Workbook.Calculate();
                String Sql = getRealStatement();
                Console.WriteLine(Sql);
                try
                {
                    DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql, "");
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

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
