using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data.PopUpAction
{
    public class PopUpActionFactory
    {
        public static AbstractPopUpAction getAction(String actionName)
        {
            AbstractPopUpAction action = null;
            actionName = "XSheet.Data.PopUpAction.PopUpAction" + actionName;
            //XNamedTable
            try
            {
                Type type = Type.GetType(actionName, true);
                action = (AbstractPopUpAction)Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                System.Windows.Forms.MessageBox.Show("非法类型：" + actionName);
            }
            return action;
        }
    }
}
