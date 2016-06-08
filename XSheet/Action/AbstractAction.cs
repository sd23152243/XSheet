using System;
using XSheet.Data;

namespace XSheet.Action
{
    abstract class AbstractAction
    {
        XNamedRange sRange { get; set; }
        XNamedRange dRange { get; set; }
        abstract public String doAction(XRange selectedRange);
        

    }
}
