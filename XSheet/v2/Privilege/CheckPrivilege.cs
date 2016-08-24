using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Privilege
{
    interface CheckPrivilege
    {
        List<string> getPrivilegeList();
        String CheckPrivilegeList(String App, String Sheet,String User);
    }
}
