using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
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
        abstract public String doAction(Range selectedNamed);
        abstract public void init();
    }
}
