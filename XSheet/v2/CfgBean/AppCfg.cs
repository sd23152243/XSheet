using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.CfgBean
{
    public class AppCfg
    {
        //AppID	AppName	Version	FileLocation	TimedCommand	TimedInterval
        public String AppID { get; set; } 
        public String AppName { get; set; }
        public String Version { get; set; }
        public String FileLocation { get; set; }
        public String TimedCommand { get; set; }
        public String TimedInterval { get; set; }
    }
}
