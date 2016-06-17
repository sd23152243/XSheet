using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;

namespace XSheet.Action
{
    class XCommand
    {
        public String CommandName { get; set; }

        public Dictionary<int, AbstractAction> actions
        {
            get
            {
                return actions;
            }

            set
            {
                actions = value.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            }
        }


        public String execute(XRange range)
        {
            foreach (KeyValuePair<int, AbstractAction> kv in actions)
            {
                kv.Value.doAction(range);
            }
            return "success";
        }
    }
}
