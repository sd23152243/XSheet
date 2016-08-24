using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Util;

namespace XSheet.v2.CfgBean
{
    public class XCfgData
    {
        Worksheet cfgsheet { get; set; }
        Dictionary<String,Table> cfgtables { get; set; }
        public AppCfg app { get; set; }
        public List<SheetCfg> sheets { get; set; }
        public List<DataCfg> datas { get; set; }
        public List<CommandCfg> commands { get; set; }
        public List<ActionCfg> actions { get; set; }
        public String flag;
        public XCfgData(Worksheet cfgsheet)
        {
            cfgtables = new Dictionary<string, Table>();
            flag = "OK";
            this.cfgsheet = cfgsheet;
            TableCollection tables = cfgsheet.Tables;
            foreach (Table table in tables)
            {
                cfgtables.Add(table.Name.ToUpper(), table);
            }
            InitCfg();
        }
        //配置文件初始化读取
        public void InitCfg()
        {
            initApp();
            initData();
            initSheet();
            initCommand();
            initAction();
       }
        //读取app初始化信息
        private void initApp()
        {
            this.app = new AppCfg();
            Table table = getCfgTable("CFG_APP");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            app.AppID = table.DataRange[0, 0].DisplayText;
            app.AppName = table.DataRange[0, 1].DisplayText;
            app.Version = table.DataRange[0, 2].DisplayText;
            app.FileLocation = table.DataRange[0, 3].DisplayText;
            flag = "OK";
        }
        
        //读取range初始化信息
        private void initData()
        {
            Table table = getCfgTable("CFG_DATA");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            datas = new List<DataCfg>();
            for (int i = 1; i < table.Range.RowCount; i++)
            {
                if (table.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                DataCfg data = new DataCfg();
                //DataName	ObjectName	ObjectType	DBName	ServerName	BaseSQLStatement	RangeName	CRUDP	SVK	InitStatement
                data.DataName = table.Range[i, 0].DisplayText;
                data.ObjectName = table.Range[i, 1].DisplayText;
                data.ObjectType = table.Range[i, 2].DisplayText;
                data.DBName = table.Range[i, 3].DisplayText;
                data.ServerName = table.Range[i, 4].DisplayText;
                data.BaseSQLStatement = table.Range[i, 5].DisplayText;
                data.RangeName = table.Range[i, 6].DisplayText;
                data.CRUDP = table.Range[i, 7].DisplayText;
                data.SVK = table.Range[i, 8].DisplayText;
                data.InitStatement = table.Range[i, 9].DisplayText;
                if (data.ServerName == null || data.ServerName.Length < 2)
                {
                    System.Windows.Forms.MessageBox.Show("区域:" + data.DataName + "服务器类型配置错误，当前配置为：" + data.ServerName);
                    flag = "NG";
                    return;
                }
                datas.Add(data);
            }
        }
        //读取Sheet初始化信息
        private void initSheet()
        {
            Table table = getCfgTable("CFG_SHEET");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            sheets = new List<SheetCfg>();
            for (int i = 0; i < table.DataRange.RowCount; i++)
            {
                if (table.DataRange[i, 0].DisplayText.Length ==0)
                {
                    break;
                }
                //SheetName	SheetDesc	CRUDP	NeedHide	NeedLog
                SheetCfg sheet = new SheetCfg();
                sheet.SheetName = table.Range[i, 0].DisplayText;
                sheet.SheetDesc = table.Range[i, 1].DisplayText;
                sheet.CRUDP = table.Range[i, 2].DisplayText;
                sheet.NeedHide = table.Range[i, 3].DisplayText;
                sheet.NeedLog = table.Range[i, 4].DisplayText;
                sheets.Add(sheet);
            }
        }
        
        //读取绑定初始化信息
        private void initCommand()
        {
            Table table = getCfgTable("CFG_COMMAND");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            commands = new List<CommandCfg>();
            for (int i = 1; i<table.Range.RowCount; i++)
            {
                if (table.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                CommandCfg command= new CommandCfg();
                //RangeName EventType   CommandName CommandDesc CRUDP Async   NeedLog
                //command.commandID = name.Range[i, 0].DisplayText;
                command.RangeName = table.Range[i, 0].DisplayText;
                command.EventType = table.Range[i, 2].DisplayText;
                command.CommandName = table.Range[i, 3].DisplayText;
                command.CommandDesc = table.Range[i, 4].DisplayText;
                command.CRUDP = table.Range[i, 4].DisplayText;
                command.Async = table.Range[i, 4].DisplayText;
                command.NeedLog = table.Range[i, 4].DisplayText;
                commands.Add(command); 
            }
        }
        //读取action初始化信息
        private void initAction()
        {
            Table table = getCfgTable("CFG_ACTION");
            if (table == null)
            {
                flag = "NG";
                return;
            }
            actions = new List<ActionCfg>();
            for (int i = 1; i < table.Range.RowCount; i++)
            {
                if (table.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                ActionCfg action = new ActionCfg();
                //CommandName	ActSeq	ActionName	ActionType	CRUDP	ActionDesc	SRange	DRange	Invalid	OnSuccess	OnFail	ActionStatement
                action.CommandName = table.Range[i, 0].DisplayText;
                action.ActSeq = table.Range[i, 1].DisplayText;
                action.ActionName = table.Range[i, 2].DisplayText;
                action.ActionType = table.Range[i, 3].DisplayText;
                action.CRUDP = table.Range[i, 4].DisplayText;
                action.ActionDesc = table.Range[i, 5].DisplayText;
                action.SRange = table.Range[i, 6].DisplayText;
                action.DRange = table.Range[i, 7].DisplayText;
                action.Invalid = table.Range[i, 7].DisplayText;
                action.OnSuccess = table.Range[i, 7].DisplayText;
                action.OnFail = table.Range[i, 7].DisplayText;
                action.ActionStatement = table.Range[i, 7].GetReferenceA1();
                actions.Add(action);
             }

        }
        private Table getCfgTable(String tableName)
        {
            Table table = null;
            try
            {
                table = cfgtables[tableName];
                if (table.Range == null)
                {
                    MessageBox.Show("名称为" + tableName + "的EXCEL Table区域配置异常，请确认配置");
                    return null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("未找到名称为" + tableName + "的EXCEL Table区域，请确认配置");

            }
            return table;
        }

    }
}
