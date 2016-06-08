using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Action
{
    interface RangeActionFactory
    {
        AbstractAction getAction(String type);
    }
}
