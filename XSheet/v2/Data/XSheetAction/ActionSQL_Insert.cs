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
using XSheet.v2.Util;

namespace XSheet.v2.Data.XSheetAction
{
    public class ActionSQL_Insert : XAction
    {
        protected override string doOwnAction()
        {
            List<String> statements = getRealStatement();
            if (statements[0] == "")
            {
                try
                {
                    dRange.doInsert();
                    
                    return "OK";
                }
                catch (Exception)
                {
                    return "FAILED";
                }
                
            }
            else
            {
                try
                {
                    dRange.ExecuteSql(statements);
                }
                catch (Exception e)
                {
                    AlertUtil.Show("error", e.ToString());
                }
            }
            dRange.Refresh();
            return "OK";
        }
    }
}
