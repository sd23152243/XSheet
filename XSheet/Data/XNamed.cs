using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
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
        private double oldSize { get; set; }
        public XSheet sheet { get; set; }
        public Dictionary<int,int> selectedRows{ get; set;}
        public Dictionary<String, XCommand> commands { get; set; }
        public String type { get; set; }
        public RangeCfgData cfg { get; set; }
        public DataTable dt { get; set; }
        public DbDataAdapter da { get; set; }
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

        abstract public int isInRange(Range range);
        abstract public int isInRange(AreasCollection areas);
        abstract public void doCommand(String eventType,AreasCollection selectedRange);
        abstract public void doResize(int rowcount, int columncount);
        public XNamed()
        {
            commands = new Dictionary<string, XCommand>();
            selectedRows = new Dictionary<int, int>();
            oldSize = -1.0;
        }

        public String getSqlStatement()
        {
            Range range = getRange().Worksheet.Workbook.Worksheets["Config"].Range[cfg.sqlStatement];
            return range[0, 0].DisplayText;
        }
        public abstract void fill(DataTable dt);

        public virtual void selectRow(int rowNum){
            if (selectedRows.ContainsKey(rowNum))
            {
                selectedRows[rowNum] += 1;
            }
            else
            {
                selectedRows.Add(rowNum, 1);
            }
            //this.getRange()[rowNum, 0].Value += ".";
            setRoweSelectStyle(rowNum);
        }

        public virtual void UnselectRow(int rowNum)
        {
            if (selectedRows.ContainsKey(rowNum))
            {
                if (selectedRows[rowNum]>0)
                {
                    selectedRows[rowNum] -= 1;
                }
                else
                {
                    selectedRows[rowNum] =0;
                }
                
            }
            else
            {
                selectedRows.Add(rowNum, 0);
            }
            setRoweSelectStyle(rowNum);
            //string value = this.getRange()[rowNum, 0].Value.ToString();
            //this.getRange()[rowNum, 0].Value = value.Substring(0,value.Length-1);
        }
        //待完善
        private void setRoweSelectStyle(int rowNum)
        {
            int realRow = rowNum;
            if (this.type.ToUpper() =="TABLE")
            {
                realRow +=1;
            }
            if (selectedRows[rowNum] % 2 == 1)
            {
                if (oldSize <0)
                {
                    this.oldSize = this.getRange()[realRow, this.getRange().ColumnCount - 1].Font.Size;
                }
                for (int i = 0; i < this.getRange().ColumnCount; i++)
                {
                    this.getRange()[realRow, i].Fill.BackgroundColor = Color.Yellow;
                }
                
            }
            else
            {
                for (int i = 0; i < this.getRange().ColumnCount; i++)
                {
                    this.getRange()[realRow, i].Fill.BackgroundColor = Color.Transparent;
                }
            }
        }

        public virtual int getRowIndexByRange(Range range)
        {
            int rowNum = -1;
            if (range.Worksheet.Name == getRange().Worksheet.Name && isInRange(range)>=0)
            {
                int r = range.TopRowIndex;
                int l = getRange().LeftColumnIndex;
                if (getRange().Worksheet[r, l].Tag != null)
                {
                    int.TryParse(getRange().Worksheet[r, l].Tag.ToString(), out rowNum);
                }
               
            }
            return rowNum;
        }

        public virtual void setSelectIndex(int rowIndex,int colIndex)
        {
            String r1c1 = cfg.sqlStatement;
            this.getRange().Worksheet.Workbook.Worksheets["Config"][r1c1].Offset(0, 4).Value = rowIndex+1;
            this.getRange().Worksheet.Workbook.Worksheets["Config"][r1c1].Offset(0, 5).Value = colIndex+1;
        }
    }
}
