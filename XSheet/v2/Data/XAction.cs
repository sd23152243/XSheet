using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using XSheet.Data;
using XSheet.v2.CfgBean;
using XSheet.v2.Data.XSheetRange;
using XSheet.v2.Util;

namespace XSheet.v2.Data
{
    //Action父类，抽象出Action的公共方法
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
            this.cfg = cfg;
            this.ActionName = cfg.ActionName;
            try
            {
                this.actionSeq = int.Parse(cfg.ActSeq);
            }
            catch
            {
                AlertUtil.Show("error",String.Format("Action {0} Seq设置异常，设置值为{1}", ActionName,cfg.ActSeq));
            }
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
        }

        public string doAction()
        {
            String ans = "OK";
            if (getValiedFlag())
            {
                ans = doOwnAction();
            }
            return ans;
        }
        //具体实现Action交由子类实现
        protected abstract string doOwnAction();
        //获取当前状态的执行语句
        protected virtual List<String> getRealStatement()
        {
            String statement = cfg.ActionStatement;

            return dRange.getRealStatement("Config_Action", statement);
        }
        //抽象类中不再实现
        public virtual void setSelectIndex(int rowIndex)
        {
            /*String r1c1 = cfg.actionStatement;
            if (r1c1.StartsWith("*"))
            {
                r1c1 = r1c1.Remove(0, 1);
                dRange.getRange().Worksheet[r1c1].Offset(0, 1).Value = rowIndex + 1;
            }*/
        }
        //读取下一个Action 序号
        internal int getNextIndex(string ans, int i)
        {
            int nextid;
            if (ans == "OK")
            {
                if (!int.TryParse( cfg.OnSuccess,out nextid))
                {
                    nextid = i + 1;
                }
            }
            else
            {
                nextid = i + 1;
            }
            return nextid;
        }
        //获取有效标记
        public Boolean getValiedFlag()
        {
            dRange.getRange().Worksheet.Workbook.Calculate();
            dRange.getRange().Worksheet.Workbook.Worksheets["Config_Action"].Calculate();//刷新工作簿及Config表
            if (cfg.Invalid == null)
            {
                //如果CONFIG中InValid配置为空，则直接认为有效
                return true;
            }
            String statement = dRange.getRange().Worksheet.Workbook.Worksheets["Config_Action"][cfg.Invalid][0].DisplayText;//获取当前情况下的实时cfg的配置显示
            return statement.Length==0||statement=="0";
        }
    }
}
