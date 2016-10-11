using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.v2.Data;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.v2.Data.XSheetAction
{
    public class ActionSQL_Delete : XAction
    {
        protected override string doOwnAction()
        {
            List<String> sqls = getRealStatement();
            if (sqls[0] == "")
            {
                dRange.doDelete();
                dRange.Refresh();
                return "OK";
            }
            else
            {
                dRange.ExecuteSql(sqls);
            }
            return "false";
        }
    }
}
