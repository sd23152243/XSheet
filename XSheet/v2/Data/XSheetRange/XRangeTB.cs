using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using XSheet.Util;
using System.Data;
using System.Drawing;
using XSheet.Data;
using XSheet.v2.CfgBean;

namespace XSheet.v2.Data.XSheetRange
{
    public class XRangeTB: XRange
    {
        private Table table;
        public Dictionary<int, int> selectedRows { get; set; }//KEY存放行号，value存放选中次数
        public Dictionary<int, int> drawRows { get; set; }//KEY存放行号，value存放选中次数
        public void setDefinedName(Worksheet sheet)
        {
            
            foreach (Table table in sheet.Tables)
            {
                if (table.Name.ToUpper().Trim() == this.Name.ToUpper().Trim())
                {
                    this.table = table;
                    break;
                }
            }
            //table.Range = getRange();
        }

        public override Range getRange()
        {
            return this.table.DataRange;
        }

        public override void doCommand(string eventType)
        {
            try
            {
                XCommand command = commands[eventType.ToUpper()];
                command.execute();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void changeDefinedRange(Range newrange)
        {
            this.table.Range = newrange;
            for (int i = 0; i < this.table.DataRange.RowCount; i++)
            {
                this.table.DataRange[i, 0].Tag = i;

            }
        }
        

        public  int getIndexAddedDataCount(int dataCount)
        {
            int rowIndex = table.Range.TopRowIndex;
            if (dataCount == 0)
            {
                rowIndex++;
            }
            else
            {
                rowIndex += dataCount;
            }
            rowIndex = rowIndex + 1;
            return rowIndex;
        }
        //1：参数区域在本区域内；2：参数区域在本区域内，但包含表头；-1参数区域不在本区域内
        //range为鼠标选择区域
        public override int isInRange(Range range)
        {
            return RangeUtil.isInRange(range, this.getRange());
        }

        public override void doResize(int rowcount, int columncount)
        {
            Range range = this.table.Range;
            String rfA1 = range.GetReferenceA1(ReferenceElement.ColumnAbsolute | ReferenceElement.RowAbsolute);

            String[] tmp = rfA1.Split('$');
            if (tmp.Length > 5)
            {
                System.Windows.Forms.MessageBox.Show("当前区域:" + Name + "定义不规范，当前定义类型为" + rfA1 + "Range类型定义应为$A$1:$C$10");
                return;
            }
            else if (tmp.Length == 3)
            {
                rfA1 = rfA1 + ":" + rfA1;
                tmp = rfA1.Split('$');
            }

            int rowIndex = getIndexAddedDataCount(rowcount);
            tmp[tmp.Length - 1] = rowIndex.ToString();
            Range newrange = range.Worksheet.Range[string.Join("$", tmp)];
            changeDefinedRange(newrange);
        }

        public override void fill(DataTable dt)
        {
            selectedRows = new Dictionary<int, int>();
            drawRows = new Dictionary<int, int>();
            Range range = getRange();
            Cell data1stcell = range[0, 0];
            string[,] arrtmp = new string[range.RowCount, range.ColumnCount];
            range.Worksheet.Import(arrtmp, data1stcell.RowIndex, data1stcell.ColumnIndex);
            range.Worksheet.Import(dt,false, data1stcell.RowIndex, data1stcell.ColumnIndex);
            this.data.setData(dt);
            range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
            /*for (int i = 0; i < range.RowCount; i++)
            {
                setRowBorderNone(i);
            }*/
            doResize(dt.Rows.Count, dt.Columns.Count);
        }

        public override String getType()
        {
            return "Table";
        }
        public override bool isSelectable()
        {
            return true;
        }

        private Range getRowRange(int rowNum)
        {
            int rowindex = rowNum + getRange().TopRowIndex;
            int lcolindex = getRange().LeftColumnIndex;
            int rcolindex = getRange().RightColumnIndex;
            Range range = getRange().Worksheet.Range.FromLTRB(lcolindex, rowindex, rcolindex, rowindex);
            return range;
        }

        //实际绘图功能，根据定义列表绘制选择标记
        protected void drawByRowList(Dictionary<int, int> tmp)
        {
            Range rangeD = null;
            Range rangeN = null;
            //rangetest.Borders.SetAllBorders(Color.Beige, BorderLineStyle.Double);*/
            foreach (var item in tmp)
            {
                int row = item.Key;
                if (item.Value % 2 == 1)
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

        //绘图接口，根据当前状况绘图,存在 RANGE 和 TABLE 冲突，后续调整架构
        public void drawSelectedRows()
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

        public override void init(DataCfg cfg)
        {
            throw new NotImplementedException();
        }

        public virtual void selectRow(int rowNum)
        {
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
                if (selectedRows[rowNum] > 0)
                {
                    selectedRows[rowNum] -= 1;
                }
                else
                {
                    selectedRows[rowNum] = 0;
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
                //range.Style.Fill.BackgroundColor = Color.Yellow;
            }

        }
        //功能，将某行设置以为无边框
        protected void setRowBorderNone(Range range)
        {
            if (range != null)
            {
                range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
                //range.Style.Fill.BackgroundColor = Color.Transparent;
            }
        }

        public virtual int getRowIndexByRange(Range range)
        {
            int rowNum = -1;
            if (range.Worksheet.Name == getRange().Worksheet.Name && isInRange(range) >= 0)
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

        public override void onSelect()
        {
            AreasCollection areas = this.dname.Range.Worksheet.Selection.Areas;
            Range srange = areas[areas.Count - 1];
            for (int row = 0; row < srange.RowCount; row++)
            {
                int rn = getRowIndexByRange(srange[row, 0]);
                if (rn >= 0)
                {
                    selectRow(rn);
                }
            }

            drawSelectedRows();//在鼠标松开后，同一绘制选择
        }

        public override void fill()
        {
            throw new NotImplementedException();
        }

        public override void fill(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
