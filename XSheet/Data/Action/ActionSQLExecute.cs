using System;
using DevExpress.XtraEditors;
using DevExpress.XtraSpreadsheet;
using DevExpress.Spreadsheet;
using XSheet.Util;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace XSheet.Data.Action
{
    class ActionSQLExecute : XAction
    {
        public override string doAction(AreasCollection selectedNamed)
        {
            Range range = dRange.getRange();
            String rangeName = dRange.Name;
            String Sql = this.cfg.actionStatement;
            List<SqlParameter> Sqlparams = new List<SqlParameter>();
            String param = this.getStatement();
            //PGR08LB.TESTPR @p1
            Sql = "CALL PGR08LB.TESTPR1('"+param+"')";
            DataTable dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql, "",param);
            //dRange.fill(dt);
            return "suucess";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
