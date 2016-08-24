using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;
using XSheet.v2.Data;

namespace XSheet
{
    public class CommandExecuter:Subject
    {
        public string executeState { get; set; }

        public void excueteCmd(XRange name,String strevent){
            this.executeState = "Executing...";
            
            Notify();
            if (name != null)
            {
                name.doCommand(strevent);
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
