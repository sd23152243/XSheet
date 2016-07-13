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
        String doAction(String type, String sql,XNamed dRange, DataTable dt, List<int> selectedRowsList);
    }
}
