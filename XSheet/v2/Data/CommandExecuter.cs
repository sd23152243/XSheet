using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;
using XSheet.v2.Data;
using XSheet.v2.Privilege;

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
        public void excueteCmd(XRange range,SysEvent e){
            this.executeState = "Executing...";
            Notify();
            if (range != null)
            {
                XCommand cmd = range.getCommandByEvent(e);
                if (cmd != null)
                {
                    cmd.execute(user);
                }
            }
            
            this.executeState = "OK";
            Notify();
        }

        public void excueteCmd(XRSheet rsheet, SysEvent e)
        {
            if (e == SysEvent.Sheet_Init)
            {
                foreach (XRange range in rsheet.ranges.Values)
                {
                    excueteCmd(range, e);
                }
            }
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
