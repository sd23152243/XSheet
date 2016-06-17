using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;
using XSheet.Util;

namespace XSheet.Data
{
    public class XApp
    {
        public String appName { get; set; }
        public String user { get; set; }
        private Dictionary<string, XSheet> sheets;
        public IWorkbook book { get; set; }
        private XApp(){}
        
        public XApp(IWorkbook book,XCfgData cfg)
        {
            this.book = book;
            init(cfg);
        }

        public Dictionary<string, XSheet> getSheets()
        {
            return sheets;
        }

        public void init(XCfgData cfg)
        {
            this.appName = cfg.app.appName+"("+cfg.app.appId+")";
            this.user = WindowsIdentity.GetCurrent().Name;
            this.sheets = new Dictionary<string, XSheet>();
            foreach (SheetCfgData sheetdata in cfg.sheets)
            {
                XSheet xsheet = new XSheet();
                xsheet.sheetName = sheetdata.sheetName;
                
                xsheet.sheet = SheetUtil.getSheetByName(xsheet.sheetName,book.Worksheets);
                xsheet.initTables();
                

            }

            
        }
    }
}
