using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using XSheet.Util;
using System.Data;
using System.Drawing;
using XSheet.Data;
using XSheet.v2.CfgBean;
using XSheet.v2.Util;
using System.Linq;

namespace XSheet.v2.Data.XSheetRange
{
    public class XRangeTB: XRange
    {
        private Table table;
        private Boolean isFilled = false;
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
            Range range = null;
            try
            {
                range =this.table.DataRange;
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show(String.Format("Table:{0} 获取区域失败",this.Name));
            }
            return range;
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

        public override void doResize(int rowcount)
        {
            if (!isFilled)
            {
                AlertUtil.Show("错误！", "Table" + Name + "需先查询才能Update");
                return;
            }
            Range range = this.table.Range;

            Range newrange = RangeUtil.rangeResize(range, rowcount);
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
            range.FillColor = Color.White;
            range.Worksheet.Import(dt,false, data1stcell.RowIndex, data1stcell.ColumnIndex);
            this.data.setData(dt);
            range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
            /*for (int i = 0; i < range.RowCount; i++)
            {
                setRowBorderNone(i);
            }*/
            this.isFilled = true;
            doResize(dt.Rows.Count+1);
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

        protected override void p_init()
        {
        }

        public virtual void selectRow(int rowNum, Boolean isMutil)
        {
            if (isMutil == false)
            {
                List<int> list = new List<int>();
                list.AddRange(selectedRows.Keys);
                foreach (int key in list)
                {
                    selectedRows[key] = 0;
                }
            }
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

        public override void onSelect(Boolean isMutil)
        {
            AreasCollection areas = this.table.Range.Worksheet.Selection.Areas;
            Range srange = null;

            if (isMutil ==true)
            {
                srange = areas[areas.Count - 1];
            }
            else
            {
                srange = areas[areas.Count - 1][0];
            }
            
            for (int row = 0; row < srange.RowCount; row++)
            {
                int rn = getRowIndexByRange(srange[row, 0]);
                if (rn >= 0)
                {
                    selectRow(rn, isMutil);
                }
            }

            drawSelectedRows();//在鼠标松开后，同一绘制选择
        }

        public override void fill()
        {
            String sql = cfg.InitStatement;
            fill(sql);
        }

        public override void fill(string sql)
        {
            data.search(getRealStatement(sql));
            fill(data.getDataTable());
        }

        protected override Boolean LocateRange(IWorkbook book)
        {
            foreach (Worksheet sheet in book.Worksheets)
            {
                foreach (Table table in sheet.Tables)
                {
                    if (table.Name == this.Name)
                    {
                        this.table = table;
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool setRange(Range range)
        {
            table.Range = range;
            return true;
        }
        //返回C R RO U D P CS CM US PM
        public override List<string> getValiedLFunList()
        {
            List<string> list = new List<string>();
            foreach (char item in cfg.CRUDP)
            {
                list.Add(item.ToString());
            }
            if (cfg.CRUDP.Contains("C"))
            {
                if (rsheet.app.ranges.ContainsKey(Name+"_CS"))
                {
                    list.Add("CS");
                }
                if (rsheet.app.ranges.ContainsKey(Name + "_CM"))
                {
                    list.Add("CM");
                }
            }
            if (cfg.CRUDP.Contains("U"))
            {
                if (rsheet.app.ranges.ContainsKey(Name + "_US"))
                {
                    list.Add("US");
                }
            }
            if (getCommandByEvent(SysEvent.Btn_Search)!= null && getCommandByEvent(SysEvent.Btn_Search).Count>1)
            {
                list.Add("RO");
            }
            if (getCommandByEvent(SysEvent.Btn_Exe)!= null )
            {
                
                if( getCommandByEvent(SysEvent.Btn_Exe).Count > 1)
                {
                    list.Add("PM");
                }
            }
            else
            {
                list.Remove("P");
            }
            return list;
        }

        public override void newData(int count)
        {
            int rowcouont = table.Range.RowCount;
            rowcouont = rowcouont + count;
            doResize(rowcouont);
        }

        public override void doUpdate()
        {
            DataTable dt = data.getDataTable();
            foreach (var item in selectedRows)
            {
                if (item.Value%2 == 0)
                {
                    continue;
                }
                DataRow row = dt.Rows[item.Key];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strRange = getRowRange(item.Key)[j].Value.ToString();
                    if (row[j].ToString() == strRange)
                    {
                        continue;
                    }
                    Type t = row[j].GetType();

                    if (t.Name == "Decimal")
                    {
                        Decimal num = Convert.ToDecimal(strRange);
                        row[j] = (object)num;
                    }
                    else
                    {
                        row[j] = strRange;
                    }
                }
            }
            data.setData(dt);
            data.update();
        }

        public override void doDelete()
        {
            DataTable dt = data.getDataTable();
            selectedRows = selectedRows.OrderByDescending(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (var item in selectedRows)
            {
                if (item.Value % 2 == 0)
                {
                    continue;
                }
                dt.Rows[item.Key].Delete();
            }
            data.setData(dt);
            data.delete();
        }

        public override void doInsert()
        {
            DataTable dt = data.getDataTable();
            int dcount = getDataTable().Rows.Count;
            int maxcount = getRange().RowCount;
            for (int i= dcount;i<maxcount;i++)
            {
                DataRow templet = dt.Rows[0];
                DataRow row = dt.NewRow();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strRange = getRowRange(i)[j].Value.ToString();
                    Type t = templet[j].GetType();

                    if (t.Name == "Decimal")
                    {
                        Decimal num = Convert.ToDecimal(strRange);
                        row[j] = (object)num;
                    }
                    else
                    {
                        row[j] = strRange;
                    }
                    
                }
                dt.Rows.Add(row);
            }
            data.setData(dt);
            data.update();
        }

        public override List<string> getSelectedValueByColIndex(int col)
        {
            List<String> list = new List<string>();
            foreach (var item in selectedRows)
            {
                if (item.Value%2 == 1)
                {
                    list.Add(getRowRange(item.Key)[col].DisplayText);
                }
            }
            return list;
        }
    }
}
