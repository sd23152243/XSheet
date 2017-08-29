using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Data
{
    /*响应事件枚举*/
    public enum SysEvent
    {
        Sheet_Init=0,
        Sheet_Change=1,
        Cell_Change=2,
        Btn_Search=3,
        Btn_Edit=4,
        Btn_Delete=5,
        Btn_New=6,
        Btn_Exe=7,
        Key_Enter=8,
        Select_Change=9,
        Btn_Cancel =10,
        Btn_Save = 11,
        Event_Search =12
    }
}
