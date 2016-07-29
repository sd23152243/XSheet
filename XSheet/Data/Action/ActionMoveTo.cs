using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data.Action
{
    class ActionMoveTo : XAction
    {
        public override string doAction()
        {
            String statement = cfg.actionStatement;
            try
            {
                Range moveToRange = dRange.getRange().Worksheet[statement];
                moveToRange.Worksheet.Workbook.Worksheets.ActiveWorksheet = moveToRange.Worksheet;
                moveToRange.Select();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return "NG";
            }
            
            return "OK";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
