using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.CfgData;
using XSheet.Data.Action;

namespace XSheet.Data
{
    public abstract class XNamed
    {
        protected DefinedName dname;
        public String Name { get; set; }
        public XSheet sheet { get; set; }
        public void setDefinedName(DefinedName dname)
        {
            this.dname = dname;
        }
        public virtual void setDefinedName(Worksheet sheet)
        {
            this.dname = sheet.Workbook.DefinedNames.GetDefinedName(Name);
        }
        public virtual Range getRange()
        {
            return dname.Range;
        }
        public Dictionary<String,XCommand> commands { get; set; }
        private String type { get; set; }
        public RangeCfgData cfg { get; set;}
        abstract public int isInRange(Range range);
        abstract public int isInRange(AreasCollection areas);
        abstract public void doCommand(String eventType,AreasCollection selectedRange);
        abstract public void doResize(int rowcount, int columncount);
        public XNamed()
        {
            commands = new Dictionary<string, XCommand>();
        }

        public String getSqlStatement()
        {
            Range range = getRange().Worksheet.Workbook.Worksheets["Config"].Range[cfg.sqlStatement];
            return range[0, 0].DisplayText;
        }
        public abstract void fill(DataTable dt);

        
    }
}
