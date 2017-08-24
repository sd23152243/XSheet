using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.CfgBean;
using XSheet.Util;

namespace XSheet.test
{
    class WMTest
    {
        public WMTest(SpreadsheetControl sheetcontrol)
        {
            DataTable dt = GetUserList();
            System.Windows.Forms.MessageBox.Show(dt.Rows[0][0].ToString());
            insertList();
            dt = GetUserList();
            //System.Windows.Forms.MessageBox.Show(dt.Rows[1][0].ToString());
        }

        public DataTable GetUserList()
        {
            string sql = "SELECT Sheet名,隐藏标志,修改标志 FROM[ABC]";
            DataTable dt = ExcelHelper.GetReader(sql,"");
            return dt;
        }

        public void insertList()
        {
            string sql = "INSERT INTO [ABC](APP,Sheet名,隐藏标志,修改标志) VALUES('ddd','ddd','BdddBB','BBB')";
            try
            {
                int result = ExcelHelper.ExecuteCommand(sql,"");
                System.Windows.Forms.MessageBox.Show(result.ToString());
            }
            catch (Exception e)
            {

                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            
            
        }
    }
}
