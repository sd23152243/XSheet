using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;

namespace XSheet.test
{
    class WMTest
    {
        public WMTest()
        {
            List<AppCfgData> lists1 = new List<AppCfgData>();
            Dictionary<String, AppCfgData> dic = new Dictionary<String, AppCfgData>();
            AppCfgData data = new AppCfgData();
            data.appId = "1";
            lists1.Add(data);
            data.appName = "bbb";
            data.appId = "2";
            dic.Add("bbb", data);
            System.Windows.Forms.MessageBox.Show(lists1[0].appId);
        }
    }
}
