﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.Util;
using System.Windows.Forms;
using XSheet.v2.Data;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.v2.Data.XSheetAction
{
    public class ActionSQL_Insert : XAction
    {
        public override string doAction()
        {
            if (getRealStatement() == "")
            {
                dRange.doInsert();
                dRange.Refresh();
                return "OK";
            }
            return "false";
        }
    }
}
