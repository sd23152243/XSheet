using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using XSheet.Util;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.Common;
using XSheet.v2.Data;

namespace XSheet.v2.Data.XSheetAction
{
    class ActionSQL_Execute : XAction
    {
        public override string doAction()
        {
            Range range = dRange.getRange();
            String rangeName = dRange.Name;
            String Sql = getRealStatement()[0];
            List<SqlParameter> Sqlparams = new List<SqlParameter>();
            //String param = this.getStatement();
            //PGR08LB.TESTPR @p1
            //Sql = param;
            DbDataAdapter da = dRange.getDbDataAdapter(Sql);
            da.SelectCommand.ExecuteNonQuery();
            //DataTable dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql, "",param);
            //dRange.fill(dt);
            return "suucess";
        }
    }
}
