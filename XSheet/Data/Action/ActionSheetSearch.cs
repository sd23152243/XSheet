using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;

namespace XSheet.Data.Action
{
    class ActionSheetSearch : XAction
    {
        public override string doAction()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("col0", System.Type.GetType("System.String"));
            String Statement = getRealStatement();
            List<String> infomList = this.dRange.sheet.app.getDocumentInfo(Statement);
            if (infomList == null)
            {
                return "error";
            }
            foreach (String item in infomList)
            {
                DataRow row = dRange.dt.NewRow();
                row[0] = item;
                dt.Rows.Add(row);
            }
            dRange.fill(dt);
            return "OK";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
