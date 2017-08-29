using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet
{
    /*观察者接口，监控当前点的Command可执行状态以及用户权限*/
    public interface Observer
    {
        void UpdateCmdStatu(String statu);

        String GetUserPrivilege();
    }
}
