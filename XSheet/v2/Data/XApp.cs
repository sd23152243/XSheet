using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.v2.CfgBean;
using XSheet.Data.Action;
using XSheet.Util;
using XSheet.v2.Data.XSheetRange;
using XSheet.v2.Data;

namespace XSheet.Data
{
    public class XApp
    {
        public String appName { get; set; }
        public String user { get; set; }
        public Dictionary<string, XRSheet> rsheets { get; set; }
        public Dictionary<string, XRange> ranges { get; set; }
        public Dictionary<string, XCommand> commands { get; set; }
        public Dictionary<string, XAction> actions;
        public XCfgData cfg { get; set; }
        public IWorkbook book { get; set; }
        public String flag { get; set; }
        private XApp(){}
        
        public XApp(IWorkbook book,XCfgData cfg)
        {
            this.book = book;
            rsheets = new Dictionary<string, XRSheet>();
            ranges = new Dictionary<string, XRange>();
            commands = new Dictionary<string, XCommand>();
            actions = new Dictionary<string, XAction>();
            this.flag = "OK";
            init(cfg);
        }

        public Dictionary<string, XRSheet> getSheets()
        {
            return rsheets;
        }

        public void init(XCfgData cfg)
        {
            this.cfg = cfg;
            this.appName = cfg.app.AppName + "(" + cfg.app.AppID + ")";
            this.user = WindowsIdentity.GetCurrent().Name;
            initRange();
            initSheet();
            initCommand();
            initAction();
            BindAction();
        }

        

        private void initRange()
        {
            foreach (DataCfg cfg in cfg.datas)
            {
                XRange range = XRangeFactory.getXRange(cfg);
                try
                {
                    range.init(cfg);
                    ranges.Add(range.Name, range);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show("Range对应命名区域不存在或配置异异常，RangeId：" + cfg.RangeName);
                    return;
                }
            }
        }

        private void initSheet()
        {
            this.rsheets = new Dictionary<string, XRSheet>();
            foreach (SheetCfg sheetdata in cfg.sheets)
            {
                Worksheet sheet = getSheetByName(sheetdata.SheetName);
                XRSheet xsheet = new XRSheet(sheet);
                xsheet.hideflag = sheetdata.NeedHide;
                
                //xsheet.initTables();
                if (xsheet.sheet == null)
                {
                    flag = "NG";
                    return;
                }
                xsheet.app = this;
                xsheet.setVisable("");
                rsheets.Add(xsheet.sheetName, xsheet);
            }
            LoadRangeToSheet();
        }
        private void LoadRangeToSheet()
        {
            foreach (XRange xrange in ranges.Values)
            {
                String sheetname = xrange.getRange().Worksheet.Name;
                XRSheet xrsheet = getRSheetByName(sheetname);
                xrsheet.ranges.Add(xrange.Name, xrange);
            }
        }

        private void initCommand()
        {
            foreach (CommandCfg cmdcfg in cfg.commands)
            {
                XCommand cmd = new XCommand(cmdcfg);
                commands.Add(cmd.CommandName, cmd);
            }
            
        }

        private void initAction()
        {
            foreach (ActionCfg cfg in cfg.actions)
            {
                XAction action = ActionFactory.MakeAction(cfg,this);
                if (action == null)
                {
                    this.flag = "NG";
                    return;
                }
                
                this.actions.Add(action.ActionName, action);
            }
        }

        private void BindAction()
        {
            foreach (XAction action in actions.Values)
            {
                
            }
            
        }
        public void setSheetVisiable(String Name)
        {
            foreach (var sheet in rsheets)
            {
                sheet.Value.setVisable(Name);
            }

        }
        public XRSheet getRSheetByName(String name)
        {
            XRSheet rsheet = null;
            try
            {
                rsheet = rsheets[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Sheet：" + name + "未注册!请检查配置！");
            }
            return rsheet;
        }

        public Worksheet getSheetByName(String name)
        {
            Worksheet sheet = null;
            try
            {
                sheet = book.Worksheets[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("app中不存在sheet：" + name + "未注册!请检查配置！");
            }
            return sheet;
        }


        public XRange getRangeByName(String name)
        {
            XRange range = null;
            try
            {
                range = ranges[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("RANGE：" + name + "未注册!请检查配置！");
            }
            return range;
        }

        public XCommand getCommandByName(String name)
        {
            XCommand cmd = null;
            try
            {
                cmd = commands[name];
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Command：" + name + "未注册!请检查配置！");
            }
            return cmd;
        }
    }
}
