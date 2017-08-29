using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Data
{
    /*系统状态枚举*/
    public enum SysStatu
    {
        Designer = 0,
        Single = 1,
        Muilti = 2,
        Update = 3,
        Delete = 4,
        Insert = 5,
        Error = -1,
        AppError = -10,
        RangeError = -9,
        SheetError = -8,
        CommandError = -7,
        ActionError = -6
    }
}
