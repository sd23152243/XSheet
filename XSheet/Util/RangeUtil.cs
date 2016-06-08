using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data;

namespace XSheet.Util
{
    class RangeUtil
    {
        //判断某个区域是否在命名区域中
        static String getRangeInName(XRange range, List<XNamedRange> names)
        {
            foreach (XNamedRange name in names)
            {
                if (name.isInRange(range) == true)
                {
                    return name.rangeName;
                }
            }
            return null;
        }
    }
}
