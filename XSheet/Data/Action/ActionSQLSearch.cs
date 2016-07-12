using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using XSheet.Util;
using System.Data;

namespace XSheet.Data.Action
{
    class ActionSQLSearch : XAction
    {
        public override string doAction(AreasCollection selectedNamed)
        {
            Range range = dRange.getRange();
            String rangeName = dRange.Name;
            String Sql = dRange.getSqlStatement();
            DataTable dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql,"Text",null);
            dRange.fill(dt);
            return "suucess";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
