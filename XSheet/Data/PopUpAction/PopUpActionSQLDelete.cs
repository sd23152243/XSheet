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
    public class PopUpActionSQLDelete:InterfacePopUpAction
    {
        public XNamed dRange { get; set; }

        public string doAction(String type, String Sql, XNamed dRange, DataTable dt,List<int> selectedRowsList)
        {
            this.dRange = dRange;
            DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql,"");
            //da.MissingMappingAction = MissingMappingAction.Passthrough;
            //da.MissingSchemaAction = MissingSchemaAction.
            //DataTable ndt = SheetUtil.ExportRangeStopOnEmptyRow(this.name.getRange(), true, true);
            Range range = dRange.getRange();
            int topRowIndex = range.TopRowIndex;
            int leftColumnIndex = range.LeftColumnIndex;
            Worksheet sheet = range.Worksheet;
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
            return "OK";
        }
    }
}
