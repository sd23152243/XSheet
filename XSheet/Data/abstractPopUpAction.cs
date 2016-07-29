using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Util;

namespace XSheet.Data
{
    //PopUp界面中，区别于普通界面，执行的Action 会调用PopUpAction
    public abstract class AbstractPopUpAction
    {
        
        abstract public String doAction(XAction xAction, XNamed dRange, DataTable dt, List<int> selectedRowsList);

        protected void doSqlOnly(XAction action,Range range)
        {
            String type = action.cfg.actionParam;
            for (int row =1; row< range.RowCount;row++)
            {
                //Range range = action.dRange.getRange();
                action.setSelectIndex(range.TopRowIndex + row);
                range.Worksheet.Workbook.Calculate();
                String Sql = action.getRealStatement();
                Console.WriteLine(Sql);
                try
                {
                    DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql, "");
                    da.SelectCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show("SQL:"+Sql+"\n"+e.ToString());
                }
                
                //dt.Rows[row].Delete();
            }
        }
    }
}
