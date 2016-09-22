using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Data.Action;
using XSheet.v2.CfgBean;
using XSheet.v2.Data;
using XSheet.v2.Privilege;
using XSheet.v2.Task;

namespace XSheet.Data
{
    public class XCommand:TaskSubject
    {
        public int CommandSeq { get; set; }
        public String CommandName { get; set; }
        public CommandCfg cfg { get; set; }
        public Dictionary<int, XAction> actions{ get; set;}
        public Boolean Async { get; set;}
        public SysEvent e;
        private XRSheet rsheet = null;
        public XCommand(CommandCfg cfg)
        {
            actions = new Dictionary<int, XAction>();
            this.cfg = cfg;
            this.CommandName = cfg.CommandName;
            this.CommandSeq = int.Parse(cfg.CommandSeq);
            this.Async = cfg.Async.Length > 0 ? true : false;
            this.e = (SysEvent)Enum.Parse(typeof(SysEvent), cfg.EventType, true);
            setSheet();
        }
        private void setSheet()
        {
            XRSheet rsheet = null;
            foreach (XAction action in actions.Values)
            {
                if (rsheet== null)
                {
                    rsheet = action.sRange.rsheet;
                }
                else if (rsheet != action.sRange.rsheet)
                {
                    MessageBox.Show(String.Format("Command {0} 中 Action {1} 与之前Action sRange不一致，请确认配置！",CommandName,action.ActionName));
                    return;
                }
            }
            this.rsheet = rsheet;
        }
        public String execute(XSheetUser user)
        {
            StartNotify();
            String ans = "OK";
            int nextid=-1;
            foreach (char item in cfg.CRUDP)
            {
                ans = "No privilege!";
                return ans;
            }

            actions = actions.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (KeyValuePair<int, XAction> kv in actions)
            {
                try
                {
                    ans = kv.Value.doAction();
                    
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                    ans= "FAILED";
                    break;
                }
                
                kv.Value.dRange.getRange().Worksheet.Calculate();
            }
            FinishNotify();
            return ans;
        }

        public void MoveTo()
        {
            //TODO
        }
    }
}
