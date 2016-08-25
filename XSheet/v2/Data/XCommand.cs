﻿using DevExpress.Spreadsheet;
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
        public String CommandName { get; set; }
        public CommandCfg cfg { get; set; }
        public Dictionary<int, XAction> actions{ get; set;}
        public Boolean sync { get; set;}
        private XRSheet rsheet = null;
        public XCommand(CommandCfg cfg)
        {
            actions = new Dictionary<int, XAction>();
            this.cfg = cfg;
            //TODO
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
            String ans = "success";
            foreach (char item in cfg.CRUDP)
            {
                if (!user.getPrivilege(rsheet).Contains(item))
                {
                    ans = "No privilege!";
                    return ans;
                }
            }

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