using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.CfgData
{
    public class XSheetCfgData
    {
        Worksheet cfgsheet { get; set; }
        DefinedNameCollection names { get; set; }
        public AppCfgData app { get; set; }
        public List<SheetCfgData> sheets { get; set; }
        public List<RangeCfgData> ranges { get; set; }
        public List<BindingCfgData> bindings { get; set; }
        public List<CommandCfgData> commands { get; set; }
        public List<ActionCfgData> actions { get; set; }

        private XSheetCfgData()
        {

        }

        public XSheetCfgData(Worksheet cfgsheet)
        {
            this.cfgsheet = cfgsheet;
            names = this.cfgsheet.DefinedNames;
        }

        public void InitCfg()
        {
            names.GetDefinedName("APP");
        }
    }
}
