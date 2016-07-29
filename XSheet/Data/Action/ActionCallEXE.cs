using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data.Action
{
    class ActionCallEXE : XAction
    {
        public override string doAction()
        {
            System.Diagnostics.Process.Start(getRealStatement(), cfg.actionParam);
            return "OK";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
