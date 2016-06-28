using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;

namespace XSheet.Data.Action
{
    public class ActionSQLDelete : XAction
    {
        public override string doAction(AreasCollection selectedNamed)
        {
            String popup = dRange.Name + "_PopUp";
            XSheet xsheet = dRange.sheet.app.getSheets()[popup];
            XNamed named = xsheet.names[popup];
            DataTable dt = dRange.dt;
            named.sheet.PopUp(this.actionId, dt, dRange.selectedRows);
            return "";
        }

        public override void init()
        {
            
        }
    }
}
