using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;

namespace XSheet.v2.Privilege
{
    public class XSheetUser
    {
        public String UserName { get; set; }
        public String UserDomain { get; set; }
        public String machineName { get; set; }
        public String OSVersion { get; set; }
        public XSheetUser(String domain,String name)
        {
            this.UserName = name;
            this.UserDomain = domain;
        }
        public XSheetUser(String domain, String name,String machineName,String OSVersion)
        {
            this.UserName = name;
            this.UserDomain = domain;
            this.machineName = machineName;
            this.OSVersion = OSVersion;
        }
        public String getPrivilege(XRSheet sheet)
        {
            //TODO
            return "CRUDP";
        }

        public String getFullUserName()
        {
            return UserDomain + "\\" + UserName;
        }
    }
}
