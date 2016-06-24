using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;

namespace XSheet
{
    public class CommandExecuter:Subject
    {
        public string executeState { get; set; }

        public void excueteCmd(XNamed name,String strevent,AreasCollection areas){
            this.executeState = "Executing...";
            Notify();
            name.doCommand(strevent, areas);
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
