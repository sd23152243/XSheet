using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;

namespace XSheet.Data.Action
{
    public class ActionFactory
    {
        public static XAction getAction(ActionCfgData cfg)
        {
            XAction action = null;
            String nametype = cfg.actionType;
            
            nametype = "XSheet.Data.Action.Action" + nametype;
            //XNamedTable
            try
            {
                Type type = Type.GetType(nametype, true);
                action = (XAction)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("Action类型不存在，设置的类型为：" + cfg.actionType);
            }
            return action;
        }
    }
}
