using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.CfgBean;
using XSheet.v2.Data;

namespace XSheet.Data.Action
{
    public class ActionFactory
    {
        protected static XAction MakeAction(ActionCfg cfg,XApp app)
        {
            XAction action = null;
            String nametype = cfg.ActionType;
            
            nametype = "XSheet.v2.Data.XSheetAction.Action" + nametype;
            //XNamedTable
            try
            {
                Type type = Type.GetType(nametype, true);
                action = (XAction)Activator.CreateInstance(type);
                action.init(cfg,app);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("Action类型不存在，设置的类型为：" + cfg.ActionType+"\n"+e.ToString());
            }
            return action;
        }
    }
}
