using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;

namespace XSheet.Data
{
    public abstract class XAction
    {
        public String actionId { get; set; }
        public ActionCfgData cfg { get; set; }
        public XNamed sRange { get; set; }
        public XNamed dRange { get; set; }
        public int actionSeq { get; set; }
        public String flag = "OK";
        abstract public String doAction();
        abstract public void init();
        public virtual String getRealStatement()
        {
            String sql = "";
            if (cfg.actionStatement == null || cfg.actionStatement.Length<2)
            {
                sql = dRange.getSqlStatement();
            }
            else if (cfg.actionStatement.StartsWith("*"))
            {
                String r1a1 = cfg.actionStatement.Remove(0, 1);
                Console.WriteLine(r1a1);
                Range range = dRange.getRange().Worksheet.Workbook.Range[r1a1];
                sql= range[0, 0].DisplayText;
            }
            else{
                sql = cfg.actionStatement;
            }
            //Range range = dRange.getRange().Worksheet.Workbook.Worksheets["Config"].Range[cfg.actionParam];
            return sql;
        }

        public virtual void setSelectIndex(int rowIndex)
        {
            String r1c1 = cfg.actionStatement;
            if (r1c1.StartsWith("*"))
            {
                r1c1 = r1c1.Remove(0, 1);
                dRange.getRange().Worksheet[r1c1].Offset(0, 1).Value = rowIndex + 1;
            }
        }
    }
}
