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
            if (rowcount == 1)
            {
                rowcount = 2;
            }
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
            if (dt== null )
            {
                AlertUtil.Show("error", "查询结果为空，请确认查询语句");
                return;
            }
            Cell data1stcell = range[0, 0];
            string[,] arrtmp = new string[range.RowCount, dt.Columns.Count];
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
            getRange().FillColor = Color.White;
            
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
            int rowindex = getRange().TopRowIndex + rowNum;
            int lcolindex = getRange().LeftColumnIndex;
            int rcolindex = getRange().RightColumnIndex;
            Range range = getRange().Worksheet.Range.FromLTRB(lcolindex, rowindex, rcolindex, rowindex);
            return range;

        }
        private Range getRowRangeByIndex(int rowNum)
        {
            int rowindex = -1;
            Range rangetmp = getRange();
            for (int i = 0;i < rangetmp.RowCount;i++)
            {
                int tmp;
                try
                {
                    if (rangetmp[i, 0].Tag!= null && int.TryParse(rangetmp[i, 0].Tag.ToString(), out tmp))
                    {
                        if (tmp == rowNum)
                        {
                            rowindex = i;
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    AlertUtil.Show("error", e.ToString());
                }
               
            }
            if (rowindex>=0)
            {
                int factindex = getRange().TopRowIndex + rowindex;
                int lcolindex = getRange().LeftColumnIndex;
                int rcolindex = getRange().RightColumnIndex;
                Range range = getRange().Worksheet.Range.FromLTRB(lcolindex, factindex, rcolindex, factindex);
                return range;
            }
            else
            {
                return null;
            }
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
                        rangeD = getRowRangeByIndex(row);
                    }
                    else
                    {
                        rangeD = rangeD.Union(getRowRangeByIndex(row));
                    }
                }
                else
                {
                    if (rangeN == null)
                    {
                        rangeN = getRowRangeByIndex(row);
                    }
                    else
                    {
                        rangeN = rangeN.Union(getRowRangeByIndex(row));
                    }
                }
            }
            setRowBorderSelect(rangeD);
            setRowBorderNone(rangeN);
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

        public virtual void selectRow(int rowNum, int stat)//0表示单选，1表示多选，2表示多选且选择后无法取消
        {
            if (stat == 0)
            {
                List<int> list = new List<int>();
                list.AddRange(selectedRows.Keys);
                foreach (int key in list)
                {
                    if (key != rowNum)
                    {
                        selectedRows[key] = 0;
                    }
                }
            }
            if (selectedRows.ContainsKey(rowNum))
            {
                if (stat == 1)
                {
                    selectedRows[rowNum] += 1;
                }
                else
                {
                    selectedRows[rowNum] = 1;
                }
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
                //range.Borders.SetAllBorders(Color.DarkBlue, BorderLineStyle.Double);
                range.FillColor = Color.Gray;
            }

        }
        //功能，将某行设置以为无边框
        protected void setRowBorderNone(Range range)
        {
            if (range != null)
            {
                //range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
                range.FillColor = Color.White;
            }
        }

        private int getRowIndexByRange(Range range)
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
                    selectRow(rn, isMutil?1:0);
                }
            }

            drawSelectedRows();//在鼠标松开后，同一绘制选择
        }

        public override void onUpdateSelect()
        {
            AreasCollection areas = this.table.Range.Worksheet.Selection.Areas;
            Range srange = null;
            srange = areas[areas.Count - 1][0];
            for (int row = 0; row < srange.RowCount; row++)
            {
                int rn = getRowIndexByRange(srange[row, 0]);
                if (rn >= 0)
                {
                    selectRow(rn, 2);
                }
            }
        }

        public override String doSearch(List<String> sqls)
        {
            String sql = sqls[0];
            for(int i= 1 ; i< sqls.Count; i++)
            {
                sql = sql+ " UNION " + sqls[i];
            }
            String ans = data.search(sql);
            fill(data.getDataTable());
            return ans;
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
            List<string> cmdFunc = getCommandFunList();
            foreach (char item in cfg.CRUDP)
            {
                if (data.avaliableList.Contains(item.ToString())|| cmdFunc.Contains(item.ToString()) || item == 'P')
                {
                    list.Add(item.ToString());
                }
            }
            if (list.Contains("C"))
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
            if (list.Contains("U"))
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

        private List<string> getCommandFunList()
        {
            List<String> list = new List<string>();
            foreach (SysEvent e in commands.Keys)
            {
                int flag = 0;
                foreach (XCommand cmd in commands[e].Values)
                {
                    if (cmd.CommandName.StartsWith("dft_cmd_"))
                    {
                        flag = -1;
                        break;
                    }
                }
                if (flag ==-1)
                {
                    continue;
                }
                switch (e)
                {
                    case SysEvent.Sheet_Init:
                        break;
                    case SysEvent.Sheet_Change:
                        break;
                    case SysEvent.Cell_Change:
                        break;
                    case SysEvent.Btn_Search:
                        list.Add("R");
                        break;
                    case SysEvent.Btn_Edit:
                        list.Add("U");
                        break;
                    case SysEvent.Btn_Delete:
                        list.Add("D");
                        break;
                    case SysEvent.Btn_New:
                        list.Add("C");
                        break;
                    case SysEvent.Btn_Exe:
                        list.Add("P");
                        break;
                    case SysEvent.Key_Enter:
                        break;
                    case SysEvent.Select_Change:
                        break;
                    case SysEvent.Btn_Cancel:
                        break;
                    case SysEvent.Btn_Save:
                        break;
                    case SysEvent.Event_Search:
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        public override void newData(int count)
        {
            int rowcount = table.Range.RowCount;
            rowcount = rowcount + count;
            doResize(rowcount);
            Range range = getRowRange(getRange().RowCount-1);
            selectedRows.Add(rowcount-2,1);
            range.FillColor = Color.Yellow;
        }

        public override String doUpdate()
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
                    string strRange = getRowRangeByIndex(item.Key)[j].Value.ToString();
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
            return data.update();
        }

        public override String doDelete()
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
            return data.delete();
        }

        public override String doInsert()
        {
            DataTable dt = data.getDataTable();
            int dcount = getDataTable().Rows.Count;
            int maxcount = getRange().RowCount;
            Boolean Okflag = true;
            for (int i= dcount;i<maxcount;i++)
            {
                DataRow templet = dt.Rows[0];
                DataRow row = dt.NewRow();
                if (getRowRange(i)[0].Value.ToString().Length<1)
                {
                    break;
                }
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strRange = getRowRange(i)[j].Value.ToString();
                    Type t = templet[j].GetType();
                    if (strRange.Length == 0)
                    {
                        continue;
                    }
                    if (t.Name == "Decimal")
                    {
                        try
                        {
                            Decimal num = Convert.ToDecimal(strRange);
                            row[j] = (object)num;
                        }
                        catch (Exception)
                        {
                            AlertUtil.Show("error", String.Format("输入参数第{0}列与表数据类型不匹配", (j + 1).ToString()));
                            Okflag = false;
                        }
                    }
                    else if(t.Name == "Int32")
                    {
                        try
                        {
                            Int32 num = Convert.ToInt32(strRange);
                            row[j] = (object)num;
                        }
                        catch (Exception)
                        {
                            AlertUtil.Show("error", String.Format("输入参数第{0}列与表数据类型不匹配,需要类型为{1}", (j+1).ToString(),t.Name));
                            Okflag = false;
                        }
                    }
                    else
                    {
                        try
                        {
                            row[j] = strRange;
                        }
                        catch (Exception)
                        {
                            AlertUtil.Show("error", String.Format("输入参数第{0}列与表数据类型不匹配或参数类型未定义,需要类型为{1}", (j + 1).ToString(), t.Name));
                            Okflag = false;
                        }
                        
                    }
                    
                }
                dt.Rows.Add(row);
            }
            data.setData(dt);
            return Okflag?data.insert():"Failed";
        }

        public override List<string> getSelectedValueByColIndex(int col , String param)
        {
            List<String> list = new List<string>();
            foreach (var item in selectedRows)
            {
                if (item.Value%2 == 1)
                {
                    list.Add(param =="DATA"?data.getDataTable().Rows[item.Key][col].ToString():getRowRangeByIndex(item.Key)[col].DisplayText);
                }
            }
            return list;
        }


        public override String ExecuteSql(List<String> sqls)
        {
            return data.Execute(sqls);
        }

        public override void ResetSelected()
        {
            List<int> rows = new List<int>();
            rows.AddRange(selectedRows.Keys);
            foreach (int row in rows)
            {
                selectedRows[row] = 0; 
            }
            drawSelectedRows();
        }
    }
}
