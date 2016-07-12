using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data
{
    public interface InterfacePopUpAction
    {
        String doAction(String sql,String type,XNamed dRange, DataTable dt, List<int> selectedRowsList);
    }
}
