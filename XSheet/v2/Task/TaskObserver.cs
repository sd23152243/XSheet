﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Task
{ 
    public interface TaskObserver
    {
        void UpdateStartime();

        void UpdateEndtime();
    }
}
