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
    //APP类
    public class XApp
    {
        public String AppName { get { return cfgdata==null?"未知":cfgdata.app.AppName; } }//APP名
        public String AppID { get { return cfgdata == null ? "未知" : cfgdata.app.AppID; } }//APPID
        public Dictionary<string, XRSheet> rsheets { get; set; }//APP 包含的Sheet
        public Dictionary<string, XRange> ranges { get; set; }//APP包含的RANGE
        public Dictionary<string, XCommand> commands { get; set; }//APP包含的命令
        public Dictionary<string, XAction> actions;//APP包含的ACTION
        public XCfgData cfgdata { get; set; }//APPConfig
        public IWorkbook book { get; set; }//APP对应的工作簿
        public SysStatu statu { get; set; }//APP状态
        private XApp(){}//私有化无参构造函数，初始化必须带参
        
        public XApp(IWorkbook book,XCfgData cfg)//初始化
        {
            statu = SysStatu.Single;//默认为单选模式 
            this.book = book;
            rsheets = new Dictionary<string, XRSheet>();
            ranges = new Dictionary<string, XRange>();
            commands = new Dictionary<string, XCommand>();
            actions = new Dictionary<string, XAction>();
            try
            {
                init(cfg);//执行初始化
            }
            catch (Exception)
            {
                //MessageBox.Show(e.ToString());
                this.cfgdata = null;
                this.statu = SysStatu.SheetError;
            }
        }
        public void init(XCfgData cfgdata)
        {
            this.cfgdata = cfgdata;
           
            if ((int)statu>-10)
            {
                initRange();
                if ((int)statu > -9)//初始化Range未报错
                {
                    initSheet();
                    if ((int)statu > -8)//初始化Sheet未报错
                    {
                        initCommands();
                        if ((int)statu > -7)//初始化Command未报错
                        {
                            initActions();
                            setRangeDefault();
                        }
                    }
                    
                } 
            }
        }
        //设置默认COMMAND 和 ACTION
        private void setRangeDefault()
        {
            foreach (XRange range in ranges.Values)
            {
                if (range.cfg.CRUDP.Contains("R") && (range.getCommandByEvent(SysEvent.Btn_Search) == null|| !range.getCommandByEvent(SysEvent.Btn_Search).ContainsKey(0) ))
                {//当配置CRUDP中包含R 并且 SEARCH按钮未绑定事件 时，设置默认SEARCH COMMAND，并且绑定在 SEARCH按钮上
                    CommandCfg cmdSearch = MakeDefaultCMD("Btn_Search", range.Name, "R");
                    ActionCfg actSearch = MakeDefaultAct(cmdSearch.CommandName, range.Name, "R", "SQL_Search");
                    initCommand(cmdSearch);
                    initAction(actSearch);
                }
                if (range.cfg.CRUDP.Contains("C") && range.getCommandByEvent(SysEvent.Btn_New) == null)
                {//当配置CRUDP中包含C 并且NEW按钮未绑定事件 时，设置默认INSERT COMMAND，并且绑定在 NEW 按钮上
                    CommandCfg cmdInsert = MakeDefaultCMD("Btn_New", range.Name, "C");
                    ActionCfg actInsert = MakeDefaultAct(cmdInsert.CommandName, range.Name, "C", "SQL_Insert");
                    initCommand(cmdInsert);
                    initAction(actInsert);
                }
                if (range.cfg.CRUDP.Contains("U") && range.getCommandByEvent(SysEvent.Btn_Edit) == null)
                {//当配置CRUDP中包含U 并且EDIT按钮未绑定事件 时，设置默认UPDATE COMMAND，并且绑定在 EDIT 按钮上
                    CommandCfg cmdUpdate = MakeDefaultCMD("Btn_Edit", range.Name, "U");
                    ActionCfg actUpdate = MakeDefaultAct(cmdUpdate.CommandName, range.Name, "U", "SQL_Update");
                    initCommand(cmdUpdate);
                    initAction(actUpdate);
                }
                if (range.cfg.CRUDP.Contains("D") && range.getCommandByEvent(SysEvent.Btn_Delete) == null)
                {//当配置CRUDP中包含D 并且DELETE按钮未绑定事件 时，设置默认DELETE COMMAND，并且绑定在 DELETE 按钮上
                    CommandCfg cmdDelete = MakeDefaultCMD("Btn_Delete", range.Name, "D");
                    ActionCfg actDelete = MakeDefaultAct(cmdDelete.CommandName, range.Name, "D", "SQL_Delete");
                    initCommand(cmdDelete);
                    initAction(actDelete);
                }
            }
        }
        //设置默认COMMAND 配置
        private  CommandCfg MakeDefaultCMD(String EventType,String rangename,String crudp)
        {
            CommandCfg cfg = new CommandCfg();
            cfg.Async = "";//默认为同步
            cfg.CommandName = "dft_cmd_"+ rangename + "_"+EventType;//默认为dfg_cmd_+RANGE名+_+事件类型
            cfg.CRUDP = crudp;
            cfg.CommandDesc = "Default";
            cfg.EventType = EventType;
            cfg.NeedLog = "1";//默认为需要日志
            cfg.CommandSeq = "0";//默认 Seq号为0
            cfg.RangeName = rangename;
            return cfg;
        }
        //设置默认ACTION 配置
        private ActionCfg MakeDefaultAct(String cmdName, String rangename, String crudp,String actionType)
        {
            ActionCfg cfg = new ActionCfg();
            cfg.ActionName = "dft_act_" + rangename + "_" + crudp;//默认为dfg_act_+RANGE名+_+C/R/U/D/P标志
            cfg.ActionDesc = "";
            cfg.ActionStatement = "";
            cfg.ActionType = actionType;
            cfg.ActSeq = "1";//默认ACTION SEQ = 1
            cfg.CommandName = cmdName;
            cfg.CRUDP = crudp;
            cfg.DRange = rangename;
            cfg.SRange = rangename;
            return cfg;
        }

        //初始化RANGE
        private void initRange()
        {
            foreach (DataCfg cfg in cfgdata.datas)//遍历RANGE配置
            {
                XRange range = XRangeFactory.getXRange(cfg);//工厂模式，根据配置生成实际FORM/RANGE/TABLE 对象
                try
                {
                    range.init(cfg,book);//range 初始化
                    ranges.Add(range.Name, range);//将 range 放入集合，后续可根据range名获取range
                    if (range.getRange() == null)//如果对应的工作表中range区域 不存在，则报错，并将系统状态设置为Designer状态
                    {
                        AlertUtil.Show("error","Range对应命名区域不存在或配置异常，RangeId：" + cfg.RangeName +" 即将进入Designer模式");
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
        //Sheet的初始化
        private void initSheet()
        {
            this.rsheets = new Dictionary<string, XRSheet>();
            foreach (SheetCfg sheetdata in cfgdata.sheets)
            {
                Worksheet sheet = getSheetByName(sheetdata.SheetName);
                
                
                //xsheet.initTables();
                if (sheet == null)
                {//如果对应sheet为空，则进入设计模式
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
        //将SHEET于RANGE 绑定
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
            foreach (CommandCfg cmdcfg in cfgdata.commands)
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
            foreach (ActionCfg cfg in cfgdata.actions)
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
        //封装方法，设置Sheet的可见性
        public void setSheetVisiable(String Name)
        {
            foreach (var sheet in rsheets)
            {
                sheet.Value.setVisable(Name);
            }

        }
        //根据名称获取SHEET
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
        //根据名称获取EXCEL SHEET
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
        //根据名称获取RANGE
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
        //根据名称获取COMMAND
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
        //获取APPName
        public String getFullAppName()
        {
            return AppName + "(" + AppID + ")";
        }
    }
}
