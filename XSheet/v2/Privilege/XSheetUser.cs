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
        public XSheetUser(String domain,String name)
        {

        }
        public XSheetUser(String domain, String name,String machineName,String OSVersion)
        {

        }
        public String getPrivilege(XRSheet sheet)
        {
            //TODO
            return null;
        }

        public String getFullUserName()
        {
            return UserDomain + "\\" + UserName;
        }
    }
}
