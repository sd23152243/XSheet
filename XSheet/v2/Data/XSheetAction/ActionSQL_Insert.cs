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

namespace XSheet.v2.Data.XSheetAction
{
    public class ActionSQL_Insert : XAction
    {
        public override string doOwnAction()
        {
            List<String> statements = getRealStatement();
            if (statements[0] == "")
            {
                try
                {
                    dRange.doInsert();
                    dRange.Refresh();
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
                catch (Exception)
                {

                    throw;
                }
            }
            return "OK";
        }
    }
}
