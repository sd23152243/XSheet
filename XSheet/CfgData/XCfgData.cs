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
        public List<CmdActRelatedCfgData> cmdacts { get; set; }
        public XCfgData(){}
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
            initRange();
            initBinding();
            initCommand();
            initAction();
            initCmdAction();
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
        private void initRange()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Range", names);
            ranges = new List<RangeCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                RangeCfgData range = new RangeCfgData();
                range.rangeId = name.Range[i, 0].DisplayText;
                range.sheetName = name.Range[i, 1].DisplayText;
                range.crudpFlag = name.Range[i, 2].DisplayText;
                range.sqlStatement = name.Range[i, 3].GetReferenceA1();
                range.sqlPopUp = name.Range[i, 4].DisplayText;
                range.serverName = name.Range[i, 5].DisplayText;
                range.rangeType = name.Range[i, 6].DisplayText;
                ranges.Add(range);
            }

        }

        private void initBinding()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_Binding", names);
            bindings = new List<BindingCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
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
         commands = new List<CommandCfgData>();
            for (int i = 1; i<name.Range.RowCount; i++)
            {
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
            actions = new List<ActionCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                ActionCfgData action = new ActionCfgData();
                //action.commandId = name.Range[i, 0].DisplayText;
                action.actionId= name.Range[i, 0].DisplayText;
                action.actionName= name.Range[i, 1].DisplayText;
                action.actionType= name.Range[i, 2].DisplayText;
                action.actionDesc= name.Range[i, 3].DisplayText;
                action.actionSRange= name.Range[i, 4].DisplayText;
                action.actionDRange= name.Range[i, 5].DisplayText;
                action.actionStatement= name.Range[i, 6].DisplayText;
                action.actionParam= name.Range[i, 7].GetReferenceA1();//Param为动态
                actions.Add(action);
             }

        }

        private void initCmdAction()
        {
            DefinedName name = SheetUtil.getNameinNames("CFG_ComActRelated", names);
            cmdacts = new List<CmdActRelatedCfgData>();
            for (int i = 1; i < name.Range.RowCount; i++)
            {
                CmdActRelatedCfgData cmdact = new CmdActRelatedCfgData();
                cmdact.commandId = name.Range[i, 0].DisplayText;
                cmdact.actionId = name.Range[i, 1].DisplayText;
                cmdact.actionSeq = name.Range[i, 2].DisplayText;
                cmdacts.Add(cmdact);
            }

        }



    }
}
