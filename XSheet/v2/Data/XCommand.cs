using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data.Action;
using XSheet.v2.CfgBean;
using XSheet.v2.Data;
using XSheet.v2.Task;

namespace XSheet.Data
{
    public class XCommand:TaskSubject
    {
        public String CommandName { get; set; }

        public Dictionary<int, XAction> actions{ get; set;}
        public Boolean sync { get; set;}
        public XCommand(CommandCfg cfg)
        {
            actions = new Dictionary<int, XAction>();
            //TODO
        }

        public String execute()
        {
            StartNotify();
            String ans = "success";
            actions = actions.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (KeyValuePair<int, XAction> kv in actions)
            {
                try
                {
                    kv.Value.doAction();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                    ans= "false";
                    break;
                }
                
                kv.Value.dRange.getRange().Worksheet.Calculate();
            }
            FinishNotify();
            return ans;
        }
    }
}
