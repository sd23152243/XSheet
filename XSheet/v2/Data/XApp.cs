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
using System.Text.RegularExpressions;
using XSheet.v2.Util;

namespace XSheet.Data
{
    public class XApp
    {
        public String AppName { get { return cfg.app.AppName; } }
        public String AppID { get { return cfg.app.AppID; } }
        public Dictionary<string, XRSheet> rsheets { get; set; }
        public Dictionary<string, XRange> ranges { get; set; }
        public Dictionary<string, XCommand> commands { get; set; }
        public Dictionary<string, XAction> actions;
        public XCfgData cfg { get; set; }
        public IWorkbook book { get; set; }
        public SysStatu statu { get; set; }
        private XApp(){}
        
        public XApp(IWorkbook book,XCfgData cfg)
        {
            statu = SysStatu.Single;
            this.book = book;
            rsheets = new Dictionary<string, XRSheet>();
            ranges = new Dictionary<string, XRange>();
            commands = new Dictionary<string, XCommand>();
            actions = new Dictionary<string, XAction>();
            init(cfg);
        }
        public void init(XCfgData cfg)
        {
            this.cfg = cfg;
           
            if ((int)statu>-10)
            {
                initRange();
                if ((int)statu > -9)
                {
                    initSheet();
                    if ((int)statu > -8)
                    {
                        initCommands();
                        if ((int)statu > -7)
                        {
                            initActions();
                            setRangeDefault();
                        }
                    }
                    
                } 
            }
        }

        private void setRangeDefault()
        {
            foreach (XRange range in ranges.Values)
            {
                if (range.cfg.CRUDP.Contains("R") && (range.getCommandByEvent(SysEvent.Btn_Search) == null|| !range.getCommandByEvent(SysEvent.Btn_Search).ContainsKey(0) ))
                {
                    CommandCfg cmdSearch = MakeDefaultCMD("Btn_Search", range.Name, "R");
                    ActionCfg actSearch = MakeDefaultAct(cmdSearch.CommandName, range.Name, "R", "SQL_Search");
                    initCommand(cmdSearch);
                    initAction(actSearch);
                }
                if (range.cfg.CRUDP.Contains("C") && range.getCommandByEvent(SysEvent.Btn_New) == null)
                {
                    CommandCfg cmdInsert = MakeDefaultCMD("Btn_New", range.Name, "C");
                    ActionCfg actInsert = MakeDefaultAct(cmdInsert.CommandName, range.Name, "C", "SQL_Insert");
                    initCommand(cmdInsert);
                    initAction(actInsert);
                }
                if (range.cfg.CRUDP.Contains("U") && range.getCommandByEvent(SysEvent.Btn_Edit) == null)
                {
                    CommandCfg cmdUpdate = MakeDefaultCMD("Btn_Edit", range.Name, "U");
                    ActionCfg actUpdate = MakeDefaultAct(cmdUpdate.CommandName, range.Name, "U", "SQL_Update");
                    initCommand(cmdUpdate);
                    initAction(actUpdate);
                }
                if (range.cfg.CRUDP.Contains("C") && range.getCommandByEvent(SysEvent.Btn_Delete) == null)
                {
                    CommandCfg cmdDelete = MakeDefaultCMD("Btn_Delete", range.Name, "D");
                    ActionCfg actDelete = MakeDefaultAct(cmdDelete.CommandName, range.Name, "D", "SQL_Delete");
                    initCommand(cmdDelete);
                    initAction(actDelete);
                }
            }
        }

        private  CommandCfg MakeDefaultCMD(String EventType,String rangename,String crudp)
        {
            CommandCfg cfg = new CommandCfg();
            cfg.Async = "";
            cfg.CommandName = "dft_cmd_"+ rangename + "_"+EventType;
            cfg.CRUDP = crudp;
            cfg.CommandDesc = "Default";
            cfg.EventType = EventType;
            cfg.NeedLog = "1";
            cfg.CommandSeq = "0";
            cfg.RangeName = rangename;
            return cfg;
        }

        private ActionCfg MakeDefaultAct(String cmdName, String rangename, String crudp,String actionType)
        {
            ActionCfg cfg = new ActionCfg();
            cfg.ActionName = "dft_cmd_" + rangename + "_" + crudp;
            cfg.ActionDesc = "";
            cfg.ActionStatement = "";
            cfg.ActionType = actionType;
            cfg.ActSeq = "1";
            cfg.CommandName = cmdName;
            cfg.CRUDP = crudp;
            cfg.DRange = rangename;
            cfg.SRange = rangename;
            return cfg;
        }


        private void initRange()
        {
            foreach (DataCfg cfg in cfg.datas)
            {
                XRange range = XRangeFactory.getXRange(cfg);
                try
                {
                    range.init(cfg,book);
                    ranges.Add(range.Name, range);
                    if (range.getRange() == null)
                    {
                        MessageBox.Show("Range对应命名区域不存在或配置异异常，RangeId：" + cfg.RangeName );
                        statu = SysStatu.Designer;
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    MessageBox.Show("Range对应命名区域不存在或配置异异常，RangeId：" + cfg.RangeName+"\n"+e.ToString());
                    this.statu = SysStatu.RangeError;
                    return;
                }
            }
            if (ranges.Count == 0)
            {
                this.statu = SysStatu.Designer;
                AlertUtil.Show("info","未查询到有效DATA，进入设计模式！");
            }
        }

        private void initSheet()
        {
            this.rsheets = new Dictionary<string, XRSheet>();
            foreach (SheetCfg sheetdata in cfg.sheets)
            {
                Worksheet sheet = getSheetByName(sheetdata.SheetName);
                
                
                //xsheet.initTables();
                if (sheet == null)
                {
                    statu = SysStatu.Designer;
                    return;
                }
                XRSheet xsheet = new XRSheet(sheet);
                xsheet.hideflag = sheetdata.NeedHide;
                xsheet.app = this;
                xsheet.setVisable("");
                rsheets.Add(xsheet.sheetName, xsheet);
            }
            LoadRangeToSheet();
        }
        private void LoadRangeToSheet()
        {
            if (statu>0)
            {
                foreach (XRange xrange in ranges.Values)
                {
                    String sheetname = xrange.getRange().Worksheet.Name;
                    XRSheet xrsheet = getRSheetByName(sheetname);
                    xrsheet.ranges.Add(xrange.Name, xrange);
                    xrange.rsheet = xrsheet;
                }
            }
            
        }
        //初始化全部command
        private void initCommands()
        {
            foreach (CommandCfg cmdcfg in cfg.commands)
            {
                initCommand(cmdcfg);
            }
        }
        //根据CfgCommand 初始化单个command
        private void initCommand(CommandCfg cmdcfg)
        {
            XCommand cmd = new XCommand(cmdcfg);
            commands.Add(cmd.CommandName, cmd);
            XRange range = getRangeByName(cmd.cfg.RangeName);
            range.setCommand(cmd);
        }
        //初始化全部配置Action
        private void initActions()
        {
            statu = SysStatu.ActionError;
            foreach (ActionCfg cfg in cfg.actions)
            {
                initAction(cfg);
            }
            statu = SysStatu.Single;
        }
        //根据ActionCfg初始化单个Action
        private void initAction(ActionCfg cfg)
        {
            XAction action = ActionFactory.MakeAction(cfg, this);
            if (action == null)
            {
                statu = SysStatu.Error;
                return;
            }
            XCommand cmd = getCommandByName(cfg.CommandName);
            try
            {
                action.cmd = cmd;
                cmd = getCommandByName(cfg.CommandName);
                cmd.actions.Add(action.actionSeq, action);
            }
            catch (Exception)
            {

                MessageBox.Show("Action：" + action.ActionName + "与命令：" + cfg.CommandName + "绑定失败，请检查Action配置是否正确！");
                return;
            }
            this.actions.Add(action.ActionName, action);
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
                rsheet = new XRSheet(book.Worksheets.ActiveWorksheet);
                AlertUtil.Show("error","Sheet：" + name + "未注册!请检查配置！");
                statu = SysStatu.Designer;
                if (rsheet.sheetName != name)
                {
                    return null;
                }
                rsheets.Add(rsheet.sheet.Name, rsheet);
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
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("RANGE：" + name + "未注册!请检查配置！");
                MessageBox.Show(e.ToString());
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

        public String getFullAppName()
        {
            return AppName + "(" + AppID + ")";
        }
    }
}
