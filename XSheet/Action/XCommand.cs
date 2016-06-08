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

        internal Dictionary<int, AbstractAction> Actions
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

        private Dictionary<int, AbstractAction> actions;

        public String execute(XRange range)
        {
            foreach (KeyValuePair<int, AbstractAction> kv in actions)
            {
                kv.Value.doAction(range);
            }
            return "sucess";
        }
    }
}
