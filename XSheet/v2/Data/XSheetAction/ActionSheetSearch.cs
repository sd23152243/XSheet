using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    class ActionSheetSearch : XAction
    {
        protected override string doOwnAction()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("col0", System.Type.GetType("System.String"));
            String Statement = getRealStatement()[0];
            List<String> infomList = null;//TODO this.dRange.sheet.app.getDocumentInfo(Statement);
            if (infomList == null)
            {
                return "error";
            }
            foreach (String item in infomList)
            {
                DataRow row = dt.NewRow();
                row[0] = item;
                dt.Rows.Add(row);
            }
            dRange.fill(dt);
            return "OK";
        }
    }
}
