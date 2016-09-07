using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.CfgBean
{
    public class SheetCfg
    {
        //SheetName	SheetDesc	CRUDP	NeedHide	NeedLog
        public String SheetName { get; set; }
        public String SheetDesc { get; set; }
        public String CRUDP { get; set; }
        public String NeedHide { get; set; }
        public String NeedLog { get; set; }
    }
}
