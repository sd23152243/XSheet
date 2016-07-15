using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.CfgData;
using XSheet.Data.Action;
using XSheet.Util;

namespace XSheet.Data
{
    public class XApp
    {
        public String appName { get; set; }
        public String user { get; set; }
        private Dictionary<string, XSheet> sheets;
        public Dictionary<string, XNamed> names;
        private Dictionary<string, XCommand> commands;
        public Dictionary<string, XAction> actions;
        public XCfgData cfg { get; set; }
        public IWorkbook book { get; set; }
        public String flag { get; set; }
        private XApp(){}
        
        public XApp(IWorkbook book,XCfgData cfg)
        {
            this.book = book;
            sheets = new Dictionary<string, XSheet>();
            names = new Dictionary<string, XNamed>();
            commands = new Dictionary<string, XCommand>();
            actions = new Dictionary<string, XAction>();
            this.flag = "OK";
            init(cfg);
        }

        public Dictionary<string, XSheet> getSheets()
        {
            return sheets;
        }

        public void init(XCfgData cfg)
        {
            this.cfg = cfg;
            this.appName = cfg.app.appName + "(" + cfg.app.appId + ")";
            this.user = WindowsIdentity.GetCurrent().Name;
            initSheet();
            initNamed();
            initCommand();
            initAction();
            bingdAction();
        }
        private void initSheet()
        {
            this.sheets = new Dictionary<string, XSheet>();
            foreach (SheetCfgData sheetdata in cfg.sheets)
            {
                XSheet xsheet = new XSheet();
                xsheet.sheetName = sheetdata.sheetName;
                xsheet.hideflag = sheetdata.hideFlag;
                xsheet.sheet = SheetUtil.getSheetByName(xsheet.sheetName, book.Worksheets);
                //xsheet.initTables();
                if (xsheet.sheet == null)
                {
                    flag = "NG";
                    return;
                }
                xsheet.app = this;
                xsheet.setVisable();
                sheets.Add(xsheet.sheetName, xsheet);
            }
        }

        private void initNamed()
        {
            foreach (RangeCfgData rangedata in cfg.ranges)
            {
                if (sheets.ContainsKey(rangedata.sheetName))
                {
                    XSheet xsheet = sheets[rangedata.sheetName];
                    XNamed named = XNamedFactory.getXNamed(rangedata);
                    try
                    {
                        named.Name = rangedata.rangeId;
                        named.cfg = rangedata;
                        named.setDefinedName(xsheet.sheet);
                        named.cfg = rangedata;
                        named.type = named.cfg.rangeType;
                        named.sheet = xsheet;
                        xsheet.names.Add(named.Name, named);
                        names.Add(named.Name, named);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        MessageBox.Show("Sheet:\"" + rangedata.sheetName + "\"中Range对应命名区域不存在或配置异异常，RangeId：" + rangedata.rangeId);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(rangedata.rangeId + "配置的Sheet：" + rangedata.sheetName + "不存在");
                }
            }
        }
        private void initCommand()
        {
            foreach (CommandCfgData cmdcfg in cfg.commands)
            {
                XCommand cmd = new XCommand();
                cmd.CommandId = cmdcfg.commandID;
                commands.Add(cmd.CommandId, cmd);
            }
            foreach (BindingCfgData bind in cfg.bindings)
            {
                String strCmd = bind.commandId;
                String strRangeName = bind.rangeName;
                try
                {
                    XCommand cmd = commands[strCmd];
                    XNamed xname = names[strRangeName];
                    xname.commands.Add(bind.eventType.ToUpper(), cmd);
                }
                catch (Exception)
                {

                    MessageBox.Show("命令:"+strCmd+"绑定区域:"+ strRangeName + "失败，请检查命令与绑定区域配置是否正确！");
                    return;
                }
            }
        }

        private void initAction()
        {
            foreach (ActionCfgData straction in cfg.actions)
            {
                XAction action = ActionFactory.getAction(straction);
                if (action == null)
                {
                    this.flag = "NG";
                    return;
                }
                action.actionId = straction.actionId;
                action.init();
                action.cfg = straction;
                this.actions.Add(action.actionId, action);
                
                try
                {
                    XNamed actionnames = null;
                    XNamed actionnamed = null;

                    if (straction.actionSRange.Length>0)
                    {
                        actionnames = names[straction.actionSRange];
                    }
                    if (straction.actionDRange.Length>0)
                    {
                        actionnamed = names[straction.actionDRange];
                    }
                    action.dRange = actionnamed;
                    action.sRange = actionnames;
                }
                catch (Exception)
                {
                    MessageBox.Show("Action："+action.actionId+"的sRange、dRange配置错误！");
                    return;
                } 
            }
        }

        private void bingdAction()
        {
            foreach (CmdActRelatedCfgData strcmdact in cfg.cmdacts)
            {
                int num = 0;
                Int32.TryParse(strcmdact.actionSeq, out num);
                try
                {
                    XCommand actioncmd = commands[strcmdact.commandId];
                    XAction action = actions[strcmdact.actionId];
                    action.actionSeq = int.Parse(strcmdact.actionSeq);
                    actioncmd.actions.Add(action.actionSeq, action);
                }
                catch (Exception)
                {

                    MessageBox.Show("Action：" + strcmdact.actionId + "与命令：" + strcmdact.commandId + "绑定失败，请检查Action配置是否正确！");
                    return;
                }
            }
            
        }
        public void setSheetVisiable()
        {
            foreach (var sheet in sheets)
            {
                sheet.Value.setVisable();
            }

        }
        public List<String> getDocumentInfo(String Statement)
        {
            return null;
        }
    }
}
