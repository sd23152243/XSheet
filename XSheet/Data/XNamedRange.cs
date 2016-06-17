using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Action;

namespace XSheet.Data
{
    public class XNamedRange:RangeActionFactory
    {
        public String rangeName { get; set; }
        public List<XRange> ranges { get; set; }
        public Dictionary<string,AbstractAction> actions { get; set; }
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
