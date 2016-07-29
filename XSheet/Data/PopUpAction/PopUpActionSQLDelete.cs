using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Util;

namespace XSheet.Data.PopUpAction
{
    public class PopUpActionSQLDelete:AbstractPopUpAction
    {
        public XNamed dRange { get; set; }

        public override string doAction(XAction action, XNamed dRange, DataTable dt,List<int> selectedRowsList)
        {
            String type = action.dRange.cfg.serverName;
            this.dRange = dRange;
            if (action.cfg.actionStatement== null || action.cfg.actionStatement.Length<2)//第一种情况， 当ActionStatement为空时，依据dRange中Select语句执行
            {
                String Sql = action.getRealStatement();
                DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql, "");
                //da.MissingMappingAction = MissingMappingAction.Passthrough;
                //da.MissingSchemaAction = MissingSchemaAction.
                //DataTable ndt = SheetUtil.ExportRangeStopOnEmptyRow(this.name.getRange(), true, true);
                /*Range range = dRange.getRange();
                int topRowIndex = range.TopRowIndex;
                int leftColumnIndex = range.LeftColumnIndex;
                Worksheet sheet = range.Worksheet;*/
                foreach (int row in selectedRowsList)
                {
                    dt.Rows[row].Delete();
                }
                try
                {
                    da.Update(dt.GetChanges());
                    dt.AcceptChanges();
                }
                catch (SqlException ee)
                {
                    MessageBox.Show(ee.Message);
                    dt.RejectChanges();
                }
            }
            else//当ActionStatement不为空时，变更执行模式，根据实际数据，每行依次向配置文件末端插入行号，依次执行ActionStateMent
            {
                doSqlOnly(action, dRange.getRange());
            }
            
            return "OK";
        }
    }
}
