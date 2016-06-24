using DevExpress.Spreadsheet;
using DevExpress.XtraSpreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;

namespace XSheet.test
{
    class WMTest
    {
        public WMTest(SpreadsheetControl sheetcontrol)
        {
            Range range = sheetcontrol.Document.Range["RG_Container"];
            range.FillColor = System.Drawing.Color.Blue;
            
        }
    }
}
