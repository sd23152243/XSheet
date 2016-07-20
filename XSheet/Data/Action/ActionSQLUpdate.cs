using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using System.Data;
using XSheet.Util;
using System.Windows.Forms;

namespace XSheet.Data.Action
{
    public class ActionSQLUpdate : XAction
    {
        public override string doAction(AreasCollection selectedNamed)
        {
            String popup = dRange.Name + "_SQLUpdate";
            XSheet xsheet = null;
            try
            {
                xsheet = dRange.sheet.app.getSheets()[popup];
            }
            catch (Exception)
            {
                try
                {
                    popup = dRange.Name + "_PopUp";
                    xsheet = dRange.sheet.app.getSheets()[popup];
                }
                catch (Exception)
                {
                    MessageBox.Show("区域："+dRange.Name+"不存在");
                    return "NG";
                }
            }
            
            XNamed named = xsheet.names[popup];
            DataTable dt = dRange.dt;
            /*if (cfg.actionStatement == "R")
            {
                String Sql = dRange.getSqlStatement();
                dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql);
            }
            else
            {
                String Sql = cfg.actionStatement;
                dt = DBUtil.getDataTable(dRange.cfg.serverName, Sql);
            }*/
            named.sheet.PopUp(this.actionId,dt, dRange.selectedRows);
            return "OK";
        }

        public override void init()
        {
            //throw new NotImplementedException();
        }
    }
}
