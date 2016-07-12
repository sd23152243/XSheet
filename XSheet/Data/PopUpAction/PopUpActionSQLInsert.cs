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
    public class PopUpActionSQLInsert:InterfacePopUpAction
    {
        public XNamed dRange { get; set; }

        public string doAction(String Sql,String type,XNamed dRange, DataTable dt,List<int> selectedRowsList)
        {
            this.dRange = dRange;
            DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql,"");
            //da.MissingMappingAction = MissingMappingAction.Passthrough;
            //da.MissingSchemaAction = MissingSchemaAction.
            //DataTable ndt = SheetUtil.ExportRangeStopOnEmptyRow(this.name.getRange(), true, true);
            Range range = dRange.getRange();
            int i = 0;
            int topRowIndex = range.TopRowIndex;
            int leftColumnIndex = range.LeftColumnIndex;
            Worksheet sheet = range.Worksheet;
            while (sheet[i + topRowIndex + 1, leftColumnIndex].Value.ToString().Length > 0)
            {
                int rowindex = i + topRowIndex + 1;
                DataRow row = null;
                if (sheet[rowindex, leftColumnIndex].Tag != null)
                {
                    int rowListNO = int.Parse(sheet[rowindex, leftColumnIndex].Tag.ToString());
                    row = dt.Rows[selectedRowsList[i]];
                }
                else
                {
                    row = dt.NewRow();
                    dt.Rows.Add(row);
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    int colindex = j + leftColumnIndex;
                    if (row[j].ToString() != sheet[rowindex, colindex].Value.ToString())
                    {

                        Type t = row[j].GetType();

                        if (t.Name == "Decimal")
                        {
                            Decimal num = Convert.ToDecimal(sheet[rowindex, colindex].Value.ToString());
                            row[j] = (object)num;
                        }
                        else
                        {
                            row[j] = sheet[rowindex, colindex].Value.ToString();
                        }
                    }
                }
                i++;
            }

            try
            {
                da.Update(dt);
            }
            catch (SqlException ee)
            {
                MessageBox.Show(ee.Message);
            }
            return "OK";
        }
    }
}
