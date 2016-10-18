using System;

namespace XSheet.v2.CfgBean
{
    public class CommandCfg
    {
        //Range	EventType	CommandName	CommandDesc	CRUDP	Async	NeedLog
        public String RangeName { get; set; }
        public String EventType { get; set; }
        public String CommandName { get; set; }
        public String CommandDesc { get; set; }
        public String CRUDP { get; set; }
        public String Async { get; set; }
        public String CommandSeq { get; set; }
        public String NeedLog { get; set; }
    }
}
