using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Util;

namespace XSheet.CfgData
{
    public class XCfgData
    {
        Worksheet cfgsheet { get; set; }
        DefinedNameCollection names { get; set; }
        public AppCfgData app { get; set; }
        public List<SheetCfgData> sheets { get; set; }
        public List<RangeCfgData> ranges { get; set; }
        public List<BindingCfgData> bindings { get; set; }
        public List<CommandCfgData> commands { get; set; }
        public List<ActionCfgData> actions { get; set; }

        private XCfgData(){}
        public XCfgData(Worksheet cfgsheet)
        {
            this.cfgsheet = cfgsheet;
            names = this.cfgsheet.DefinedNames;
            app = new AppCfgData();
            InitCfg();


        }

        public void InitCfg()
        {
            initApp();
            initSheet();
        }

        private void initApp()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_APP",names);
            app.appId = name.Range[1, 0].DisplayText;
            app.appName = name.Range[1, 1].DisplayText;
        }

        private void initSheet()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Sheet",names);
            sheets = new List<SheetCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                SheetCfgData sheet = new SheetCfgData();
                sheet.sheetName = name.Range[i, 0].DisplayText;
                sheet.hideFlag = name.Range[i, 1].DisplayText;
                sheet.editFlag = name.Range[i, 2].DisplayText;
                sheets.Add(sheet);
            }
            
        }


       
    }
}
