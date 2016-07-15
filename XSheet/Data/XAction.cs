using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;

namespace XSheet.Data
{
    public abstract class XAction
    {
        public String actionId { get; set; }
        public ActionCfgData cfg { get; set; }
        public XNamed sRange { get; set; }
        public XNamed dRange { get; set; }
        public int actionSeq { get; set; }
        public String flag = "OK";
        abstract public String doAction(AreasCollection selectedNamed);
        abstract public void init();
        public String getStatement()
        {
            Range range = dRange.getRange().Worksheet.Workbook.Worksheets["Config"].Range[cfg.actionParam];
            return range[0, 0].DisplayText;
        }
    }
}
