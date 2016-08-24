using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.Util;
using System.Windows.Forms;
using XSheet.v2.Data;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.Data.Action
{
    public class ActionSQLUpdate : XAction
    {
        public override string doAction()
        {
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
            //named.sheet.PopUp(this.actionId,dt, dRange.selectedRows);
            return "OK";
        }
    }
}
