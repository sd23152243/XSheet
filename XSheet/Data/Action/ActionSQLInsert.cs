using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.Util;
using System.Windows.Forms;

namespace XSheet.Data.Action
{
    public class ActionSQLInsert : XAction
    {
        public override string doAction(AreasCollection selectedNamed)
        {
            String popup = dRange.Name + "_PopUp";
            XSheet xsheet = dRange.sheet.app.getSheets()[popup];
            XNamed named = xsheet.names[popup];
            DataTable dt = dRange.dt;
            /*if (cfg.actionStatement == "R")
            {
                String Sql = dRange.getSqlStatement();
                dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql);
            }
            else
            {
                String Sql = cfg.actionStatement;
                dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql);
            }*/

            named.sheet.PopUp(this.actionId,dt, null);
            return "";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
