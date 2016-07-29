﻿using DevExpress.Spreadsheet;
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
        public List<CmdActRelatedCfgData> cmdacts { get; set; }
        public XCfgData(){}
        public String flag;
        public XCfgData(Worksheet cfgsheet)
        {
            flag = "OK";
            this.cfgsheet = cfgsheet;
            names = this.cfgsheet.DefinedNames;
            app = new AppCfgData();
            InitCfg();
        }

        public void InitCfg()
        {
            initApp();
            initSheet();
            initRange();
            initBinding();
            initCommand();
            initAction();
            initCmdAction();
       }

        private void initApp()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_APP",names);
            if (name == null)
            {
                flag = "NG";
                return;
            }
            app.appId = name.Range[1, 0].DisplayText;
            app.appName = name.Range[1, 1].DisplayText;
        }

        private void initSheet()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Sheet",names);
            if (name == null)
            {
                flag = "NG";
                return;
            }
            sheets = new List<SheetCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length ==0)
                {
                    break;
                }
                SheetCfgData sheet = new SheetCfgData();
                sheet.sheetName = name.Range[i, 0].DisplayText;
                sheet.hideFlag = name.Range[i, 1].DisplayText;
                sheet.editFlag = name.Range[i, 2].DisplayText;
                sheets.Add(sheet);
            }
            
        }
        private void initRange()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Range", names);
            if (name == null)
            {
                flag = "NG";
                return;
            }
            ranges = new List<RangeCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                RangeCfgData range = new RangeCfgData();
                range.rangeId = name.Range[i, 0].DisplayText;
                range.sheetName = name.Range[i, 1].DisplayText;
                range.crudpFlag = name.Range[i, 2].DisplayText;
                //range.sqlStatement = name.Range[i, 3].GetReferenceA1(); //更新，如果动态则直接维护地址，因此直接取值
                range.sqlStatement = name.Range[i, 3].DisplayText;
                range.sqlPopUp = name.Range[i, 4].DisplayText;
                range.serverName = name.Range[i, 5].DisplayText;
                if (range.serverName == null || range.serverName.Length <2)
                {
                    System.Windows.Forms.MessageBox.Show("区域:"+range.rangeId+"服务器类型配置错误，当前配置为："+range.serverName);
                    flag = "NG";
                    return;
                }
                range.rangeType = name.Range[i, 6].DisplayText;
                ranges.Add(range);
            }

        }

        private void initBinding()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Binding", names);
            if (name == null)
            {
                flag = "NG";
                return;
            }
            bindings = new List<BindingCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                BindingCfgData binding = new BindingCfgData();
                binding.rangeName = name.Range[i, 0].DisplayText;
                binding.commandId = name.Range[i, 1].DisplayText;
                binding.eventType = name.Range[i, 2].DisplayText;
                bindings.Add(binding);
            }
        }

        private void initCommand()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Command", names);
            if (name == null)
            {
                flag = "NG";
                return;
            }
            commands = new List<CommandCfgData>();
            for (int i = 1; i<name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                CommandCfgData command= new CommandCfgData();
                command.commandID = name.Range[i, 0].DisplayText;
                command.commandName= name.Range[i, 1].DisplayText;
                command.sheetName = name.Range[i, 2].DisplayText;
                command.commandDesc = name.Range[i, 3].DisplayText;
                commands.Add(command); 
            }
        }

        private void initAction()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Action", names);
            
            if (name == null)
            {
                flag = "NG";
                return;
            }
            actions = new List<ActionCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                ActionCfgData action = new ActionCfgData();
                //action.commandId = name.Range[i, 0].DisplayText;
                action.actionId= name.Range[i, 0].DisplayText;
                action.actionName= name.Range[i, 1].DisplayText;
                action.actionType= name.Range[i, 2].DisplayText;
                action.actionDesc= name.Range[i, 3].DisplayText;
                action.actionSRange= name.Range[i, 4].DisplayText;
                action.actionDRange= name.Range[i, 5].DisplayText;
                action.actionStatement= name.Range[i, 6].DisplayText;
                //action.actionParam= name.Range[i, 7].GetReferenceA1();// 更新，如果动态则直接维护地址，因此直接取值
                action.actionParam = name.Range[i, 7].DisplayText;
                actions.Add(action);
             }

        }

        private void initCmdAction()
        {

            DefinedName name = SheetUtil.getNameinNames("CFG_ComActRelated", names);

            if (name == null)
            {
                flag = "NG";
                return;
            }
            cmdacts = new List<CmdActRelatedCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                if (name.Range[i, 0].DisplayText.Length == 0)
                {
                    break;
                }
                CmdActRelatedCfgData cmdact = new CmdActRelatedCfgData();
                cmdact.commandId = name.Range[i, 0].DisplayText;
                cmdact.actionId = name.Range[i, 1].DisplayText;
                cmdact.actionSeq = name.Range[i, 2].DisplayText;
                cmdacts.Add(cmdact);
            }

        }



    }
}
