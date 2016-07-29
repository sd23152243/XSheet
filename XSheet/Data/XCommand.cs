using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data.Action;

namespace XSheet.Data
{
    public class XCommand
    {
        public String CommandId { get; set; }

        public Dictionary<int, XAction> actions{ get; set;}
        public XCommand()
        {
            actions = new Dictionary<int, XAction>();
        }

        public String execute()
        {
            actions = actions.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (KeyValuePair<int, XAction> kv in actions)
            {
                kv.Value.doAction();
                kv.Value.dRange.getRange().Worksheet.Calculate();
            }
            
            return "success";
        }


    }
}
