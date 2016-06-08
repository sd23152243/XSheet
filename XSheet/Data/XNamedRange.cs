using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Action;

namespace XSheet.Data
{
    class XNamedRange:RangeActionFactory
    {
        String rangName { get; set; }
        List<XRange> ranges { get; set; }
        Dictionary<string,AbstractAction> actions { get; set; }
        String rangeType { get; set; }

        public AbstractAction getAction(string type)
        {
            throw new NotImplementedException();
        }


        public Boolean isInRange(XRange range)
        {
            foreach (XRange nameRange in ranges)
            {
                if (nameRange.isInRange(range)>0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
