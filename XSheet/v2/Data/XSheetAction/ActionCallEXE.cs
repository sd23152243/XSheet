using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    class ActionCallEXE : XAction
    {
        public override string doOwnAction()
        {
            System.Diagnostics.Process.Start(getRealStatement()[0]);
            return "OK";
        }
    }
}
