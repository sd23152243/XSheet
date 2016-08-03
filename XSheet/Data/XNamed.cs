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
        public Dictionary<int,int> selectedRows{ get; set;}//KEY存放行号，value存放选中次数
        public Dictionary<int, int> drawRows { get; set; }//KEY存放行号，value存放选中次数
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
            try
            {
                return dname.Range;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            
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

        public virtual String getSqlStatement()
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
            // 优化流程，不再于选择是设置勾选，改为松开鼠标后统一绘图 setRoweSelectStyle(rowNum);
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

            // setRoweSelectStyle(rowNum); 优化流程，不再于选择是设置勾选，改为松开鼠标后统一绘图
        }
        //功能，将某行设置为被选边框
        protected void setRowBorderSelect(Range range)
        {
            if (range != null)
            {
                range.Borders.SetAllBorders(Color.DarkBlue, BorderLineStyle.Double);
            }
            
        }
        //功能，将某行设置以为无边框
        protected void setRowBorderNone(Range range)
        {
            if (range != null)
            {
                range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
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
        //将当前坐标写入前台单元格
        public virtual void setSelectIndex(int rowIndex,int colIndex)
        {
            String r1c1 = cfg.sqlStatement;
            if (r1c1 .StartsWith("*"))
            {
                r1c1 = r1c1.Remove(0, 1);
                this.getRange().Worksheet[r1c1].Offset(0, 1).Value = rowIndex ;
                this.getRange().Worksheet[r1c1].Offset(0, 2).Value = colIndex ;
            }
        }
        //绘图接口，根据当前状况绘图,存在 RANGE 和 TABLE 冲突，后续调整架构
        public virtual void drawSelectedRows()
        {
            Dictionary<int, int> tmp = new Dictionary<int, int>();
            if (selectedRows == null)
            {
                selectedRows = new Dictionary<int, int>();
            }
            foreach (var rowId in selectedRows)
            {
                if (!drawRows.ContainsKey(rowId.Key))
                {
                    tmp.Add(rowId.Key, rowId.Value);
                    drawRows.Add(rowId.Key, rowId.Value);
                }
                else if (drawRows[rowId.Key] % 2 != rowId.Value)
                {
                    tmp.Add(rowId.Key, rowId.Value);
                }
                drawRows[rowId.Key] = rowId.Value;
            }
            drawByRowList(tmp);
        }
        //实际绘图功能，根据定义列表绘制选择标记
        protected  void drawByRowList(Dictionary<int, int> tmp)
        {
            Range rangeD = null ;
            Range rangeN = null ;
            //rangetest.Borders.SetAllBorders(Color.Beige, BorderLineStyle.Double);*/
            foreach (var item in tmp)
            {
                int row = item.Key + 1;
                if (item.Value%2 == 1)
                {
                    if (rangeD == null)
                    {
                        rangeD = getRowRange(row);
                    }
                    else
                    {
                        rangeD = rangeD.Union(getRowRange(row));
                    }
                }
                else
                {
                    if (rangeN == null)
                    {
                        rangeN = getRowRange(row);
                    }
                    else
                    {
                        rangeN = rangeN.Union(getRowRange(row));
                    }
                }
            }
            setRowBorderSelect(rangeD);
            setRowBorderNone(rangeN);
            Table table = getRange().Worksheet.Tables[0];
            
        }

        private Range getRowRange(int rowNum)
        {
            int rowindex = rowNum + getRange().TopRowIndex;
            int lcolindex = getRange().LeftColumnIndex;
            int rcolindex = getRange().RightColumnIndex;
            Range range = getRange().Worksheet.Range.FromLTRB(lcolindex, rowindex, rcolindex, rowindex);
            return range;
        }
    }
}
