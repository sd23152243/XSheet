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
        public int isInRange(AreasCollection areas)
        {
            int i = 0;
            foreach (Range range in areas)
            {
                i = isInRange(range);
                if (i < 0)
                {
                    break;
                }
            }
            return i;
        }
        abstract public void doCommand(String eventType);
        abstract public void doResize(int rowcount, int columncount);
        public XNamed()
        {
            commands = new Dictionary<string, XCommand>();
            selectedRows = new Dictionary<int, int>();
            oldSize = -1.0;
        }

        public String getSqlStatement()
        {
            if (this.cfg.sqlStatement.StartsWith("*"))
            {
                String r1a1 = cfg.sqlStatement.Remove(0, 1);
                Console.WriteLine(r1a1);
                Range range = getRange().Worksheet.Workbook.Range[r1a1];
                return range[0, 0].DisplayText;
            }
            return cfg.sqlStatement;
            
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
        protected void setRoweSelectStyle(int rowNum)
        {
            int realRow = rowNum;
            for (int i = 0; i < this.getRange().RowCount; i++)
            {
                try
                {
                    String tmp = this.getRange()[i, 0].Tag.ToString();
                    if (tmp != null && tmp == rowNum.ToString())
                    {
                        realRow = i;
                        break;
                    }
                }
                catch (Exception)
                {
                    continue;
                }
                
            }
            /*if (this.type.ToUpper() =="TABLE")
            {
                realRow +=1;
            }*/
            if (selectedRows[rowNum] % 2 == 1)
            {
                setRowBorderMashDash(realRow);
            }
            else
            {
                setRowBorderNone(realRow);
            }
        }
        protected void setRowBorderMashDash(int realRow)
        {
            for (int i = 0; i < this.getRange().ColumnCount; i++)
            {
                this.getRange()[realRow, i].Borders.SetAllBorders(Color.Blue, BorderLineStyle.MediumDashed);
            }
        }

        protected void setRowBorderNone(int realRow)
        {
            for (int i = 0; i < this.getRange().ColumnCount; i++)
            {
                this.getRange()[realRow, i].Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
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
            if (r1c1 .StartsWith("*"))
            {
                r1c1 = r1c1.Remove(0, 1);
                this.getRange().Worksheet[r1c1].Offset(0, 1).Value = rowIndex + 1;
                this.getRange().Worksheet[r1c1].Offset(0, 2).Value = colIndex + 1;
            }
        }
    }
}
