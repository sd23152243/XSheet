using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;

namespace XSheet.Action
{
    static class XCommandBuilder
    {
        public static XCommand getCommand(Dictionary<String, XCommand> commands,String id)
        {
            return commands[id];
        }

        public static XCommand getCommand(Dictionary<String, XCommand> commands,Worksheet sheet,String Event)
        {
            return null;
        }

     }
}
