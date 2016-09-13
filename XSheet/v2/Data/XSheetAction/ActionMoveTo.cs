using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    class ActionMoveTo : XAction
    {
        public override string doAction()
        {
            String statement = getRealStatement()[0];
            try
            {
                Range moveToRange = dRange.getRange().Worksheet[statement];
                moveToRange.Worksheet.Workbook.Worksheets.ActiveWorksheet = moveToRange.Worksheet;
                moveToRange.Select();
                moveToRange.Worksheet.ScrollTo(moveToRange);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return "NG";
            }
            
            return "OK";
        }
    }
}
