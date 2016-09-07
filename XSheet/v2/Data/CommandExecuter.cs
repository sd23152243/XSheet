using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;
using XSheet.v2.Data;
using XSheet.v2.Privilege;
using XSheet.v2.Task;

namespace XSheet
{
    public class CommandExecuter:Subject
    {
        public string executeState { get; set; }
        private XSheetUser user { get; set; }
        public CommandExecuter(XSheetUser user)
        {
            this.user = user;
        }
        public void executeCmd(XRange range,SysEvent e,int id){
            
            if (range != null)
            {
                XCommand cmd = range.getCommandByEvent(e,id);
                executeCmd(cmd);   
            }
        }


        public void executeCmd(XRSheet rsheet, SysEvent e)
        {
            //TODO Sheet_Init 与Sheet_Change区分
            if (e == SysEvent.Sheet_Init|| e == SysEvent.Sheet_Change)
            {
                foreach (XRange range in rsheet.ranges.Values)
                {
                    executeCmd(range, e);
                }
            }
        }

        public void executeCmd(XRange range ,SysEvent e)
        {
            Dictionary<int, XCommand> cmds = range.getCommandByEvent(e);
            if (cmds != null )
            {
                foreach (XCommand cmd in cmds.Values)
                {
                    if (CheckPrivilege(cmd))
                    {
                        executeCmd(cmd);
                    }
                }
            }
        }

        private bool CheckPrivilege(XCommand cmd)
        {
            String privilege = observers[0].GetUserPrivilege();
            foreach (char a in cmd.cfg.CRUDP)
            {
                if (privilege.IndexOf(a)<0)
                {
                    return false;
                }
            }
            return true;
        }

        public void executeCmd(XCommand cmd)
        {
            this.executeState = "Executing...";
            Notify();
            if (cmd != null)
            {
                CommandTask task = new CommandTask(cmd, user);
                task.doTask();
            }
            this.executeState = "OK";
            Notify();
        }

        public override void Notify()
        {
            foreach (Observer ob in observers)
            {
                ob.UpdateCmdStatu(executeState);
            }
        }
    }
}
