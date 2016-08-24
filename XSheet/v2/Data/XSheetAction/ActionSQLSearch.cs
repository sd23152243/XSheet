using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using XSheet.Util;
using System.Data;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    class ActionSQLSearch : XAction
    {
        public override string doAction()
        {
            Range range = dRange.getRange();
            /*String rangeName = dRange.Name;
            String Sql = dRange.getSqlStatement();
            Console.WriteLine(Sql);*/
            //DataTable dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql,"Text",null);
            dRange.fill();
            return "suucess";
        }
    }
}
