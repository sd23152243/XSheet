using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using XSheet.Util;
using System.Data;
using XSheet.v2.Data;

namespace XSheet.v2.Data.XSheetAction
{
    class ActionSQL_Search : XAction
    {
        public override string doAction()
        {
            Range range = dRange.getRange();
            /*String rangeName = dRange.Name;
            String Sql = dRange.getSqlStatement();
            Console.WriteLine(Sql);*/
            //DataTable dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql,"Text",null);
            String statment = getRealStatement();
            if (statment == "")
            {
                dRange.doSearch();
            }
            else
            {
                dRange.doSearch(statment);
            }
            return "suucess";
        }
    }
}
