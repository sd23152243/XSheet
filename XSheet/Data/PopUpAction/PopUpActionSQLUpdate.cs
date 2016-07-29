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
    public class PopUpActionSQLUpdate:AbstractPopUpAction
    {
        public XNamed dRange { get; set; }

        public override string doAction(XAction action, XNamed dRange, DataTable dt,List<int> selectedRowsList)
        {
            String type = action.dRange.cfg.serverName;
            String Sql = action.getRealStatement();
            if (action.cfg.actionStatement == null || action.cfg.actionStatement.Length < 2)//第一种情况， 当ActionStatement为空时，依据dRange中Select语句执行
            {
                this.dRange = dRange;
                DbDataAdapter da = DBUtil.getDbDataAdapter(type, Sql, "");
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
                    dt.Clear();
                    MessageBox.Show(ee.Message);
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
