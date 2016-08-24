using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Data;
using XSheet.v2.CfgBean;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.v2.Data
{
    public abstract class XAction
    {
        public String ActionName { get; set; }
        public ActionCfg cfg { get; set; }
        public XRange sRange { get; set; }
        public XRange dRange { get; set; }
        public int actionSeq { get; set; }
        public String flag = "OK";
        public XApp app { get; set; }
        public XCommand cmd { get; set; }
        //初始化接口
        public virtual void init(ActionCfg cfg,XApp app)
        {
            //TODO
            try
            {
                if (cfg.SRange.Length > 0)
                {
                    sRange = app.getRangeByName(cfg.SRange);
                }
                if (cfg.DRange.Length > 0)
                {
                    dRange = app.getRangeByName(cfg.DRange);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Action：" + ActionName + "的sRange、dRange配置错误！");
                return;
            }


            try
            {
                this.cmd = app.getCommandByName(cfg.CommandName);
                cmd.actions.Add(actionSeq, this);
            }
            catch (Exception)
            {

                MessageBox.Show("Action：" + ActionName + "与命令：" +cfg.CommandName + "绑定失败，请检查Action配置是否正确！");
                return;
            }

        }

        public abstract string doAction();

        protected virtual String getRealStatement()
        {
            String sql = "";
            
            return sql;
        }

        public virtual void setSelectIndex(int rowIndex)
        {
            /*String r1c1 = cfg.actionStatement;
            if (r1c1.StartsWith("*"))
            {
                r1c1 = r1c1.Remove(0, 1);
                dRange.getRange().Worksheet[r1c1].Offset(0, 1).Value = rowIndex + 1;
            }*/
        }
    }
}
