﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    class ActionCallEXE : XAction
    {
        public override string doAction()
        {
            System.Diagnostics.Process.Start(getRealStatement(), getRealStatement());
            return "OK";
        }
    }
}
