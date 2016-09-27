using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.CfgBean
{   //静态库，读取配置文件
    public static class CfgDataReader
    {
        //读取APP配置
        public static AppCfg readApp(AppCfg app,Table appCfgTable)
        {
            app = new AppCfg();
            app.AppID = appCfgTable.DataRange[0, 0].DisplayText;
            app.AppName = appCfgTable.DataRange[0, 1].DisplayText;
            app.Version = appCfgTable.DataRange[0, 2].DisplayText;
            app.FileLocation = appCfgTable.DataRange[0, 3].DisplayText;
            return app;
        }
        //读取Data配置
        public static List<DataCfg> readData(List<DataCfg> datas, Table dataCfgTable)
        {
            datas = new List<DataCfg>();
            for (int i = 1; i < dataCfgTable.Range.RowCount; i++)
            {
                if (dataCfgTable.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                DataCfg data = new DataCfg();
                //DataName	ObjectName	ObjectType	DBName	ServerName	BaseSQLStatement	RangeName	CRUDP	SVK	InitStatement
                data.DataName = dataCfgTable.Range[i, 0].DisplayText;
                data.DataDescription = dataCfgTable.Range[i, 1].DisplayText;
                data.ObjectName = dataCfgTable.Range[i, 2].DisplayText;
                data.ObjectType = dataCfgTable.Range[i, 3].DisplayText;
                data.DBName = dataCfgTable.Range[i, 4].DisplayText;
                data.ServerName = dataCfgTable.Range[i, 5].DisplayText;
                data.BaseSQLStatement = dataCfgTable.Range[i, 6].GetReferenceA1();
                data.RangeName = dataCfgTable.Range[i, 7].DisplayText;
                data.CRUDP = dataCfgTable.Range[i, 8].DisplayText;
                data.SVK = dataCfgTable.Range[i, 9].DisplayText;
                data.InitStatement = dataCfgTable.Range[i, 10].GetReferenceA1();
                if ((data.ServerName == null || data.ServerName.Length < 2) && data.DataName.Split('_')[0] =="TB")
                {
                    Console.WriteLine("区域:" + data.DataName + "服务器类型配置错误，当前配置为：" + data.ServerName);
                    return null;
                }
                if (data.RangeName.Length ==0 )
                {
                    break;
                }
                datas.Add(data);
            }
            return datas;
        }
        //读取Sheet配置
        public static List<SheetCfg> readSheet(List<SheetCfg>sheets, Table sheetCfgTable)
        {
            sheets = new List<SheetCfg>();
            for (int i = 1; i < sheetCfgTable.Range.RowCount; i++)
            {
                if (sheetCfgTable.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                //SheetName	SheetDesc	CRUDP	NeedHide	NeedLog
                SheetCfg sheet = new SheetCfg();
                sheet.SheetName = sheetCfgTable.Range[i, 0].DisplayText;
                sheet.SheetDesc = sheetCfgTable.Range[i, 1].DisplayText;
                sheet.CRUDP = sheetCfgTable.Range[i, 2].DisplayText;
                sheet.NeedHide = sheetCfgTable.Range[i, 3].DisplayText;
                sheet.NeedLog = sheetCfgTable.Range[i, 4].DisplayText;
                sheets.Add(sheet);
            }
            return sheets;
        }
        //读取Command配置
        public static List<CommandCfg> readCommand(List<CommandCfg> commands, Table cmdCfgTable)
        {
            commands = new List<CommandCfg>();
            for (int i = 1; i < cmdCfgTable.Range.RowCount; i++)
            {
                if (cmdCfgTable.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                CommandCfg command = new CommandCfg();
                //RangeName EventType   CommandName CommandDesc CRUDP Async   NeedLog
                //command.commandID = name.Range[i, 0].DisplayText;
                command.RangeName = cmdCfgTable.Range[i, 0].DisplayText;
                command.EventType = cmdCfgTable.Range[i, 1].DisplayText;
                command.CommandName = cmdCfgTable.Range[i, 2].DisplayText;
                command.CommandDesc = cmdCfgTable.Range[i, 3].DisplayText;
                command.CRUDP = cmdCfgTable.Range[i, 4].DisplayText;
                command.Async = cmdCfgTable.Range[i, 5].DisplayText;
                command.CommandSeq = cmdCfgTable.Range[i, 6].DisplayText;
                command.NeedLog = cmdCfgTable.Range[i, 7].DisplayText;
                commands.Add(command);
            }
            return commands;
        }
        //读取Action配置
        public static List<ActionCfg> readAction(List<ActionCfg> actions, Table actCfgTable)
        {
            actions = new List<ActionCfg>();
            for (int i = 1; i < actCfgTable.Range.RowCount; i++)
            {
                if (actCfgTable.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                ActionCfg action = new ActionCfg();
                //CommandName	ActSeq	ActionName	ActionType	CRUDP	ActionDesc	SRange	DRange	Invalid	OnSuccess	OnFail	ActionStatement
                action.CommandName = actCfgTable.Range[i, 0].DisplayText;
                action.ActSeq = actCfgTable.Range[i, 1].DisplayText;
                action.ActionName = actCfgTable.Range[i, 2].DisplayText;
                action.ActionType = actCfgTable.Range[i, 3].DisplayText;
                action.CRUDP = actCfgTable.Range[i, 4].DisplayText;
                action.ActionDesc = actCfgTable.Range[i, 5].DisplayText;
                action.SRange = actCfgTable.Range[i, 6].DisplayText;
                action.DRange = actCfgTable.Range[i, 7].DisplayText;
                action.Invalid = actCfgTable.Range[i, 8].GetReferenceA1();
                action.OnSuccess = actCfgTable.Range[i, 9].DisplayText;
                action.OnFail = actCfgTable.Range[i, 10].DisplayText;
                action.ActionStatement = actCfgTable.Range[i, 11].GetReferenceA1();
                actions.Add(action);
            }
            return actions;
        }
    }
}
