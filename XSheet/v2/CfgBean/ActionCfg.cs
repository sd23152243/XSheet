using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.CfgBean
{
    public class ActionCfg
    {
        //CommandName	ActSeq	ActionName	ActionType	CRUDP	ActionDesc	SRange	DRange	Invalid	OnSuccess	OnFail	ActionStatement
        public String CommandName { get; set; }
        public String ActSeq { get; set; }
        public String ActionName { get; set; }
        public String ActionType { get; set; }
        public String CRUDP { get; set; }
        public String ActionDesc { get; set; }
        public String SRange { get; set; }
        public String DRange { get; set; }
        public String Invalid { get; set; }
        public String OnSuccess { get; set; }
        public String OnFail { get; set; }
        public String ActionStatement { get; set; }
    }
}
