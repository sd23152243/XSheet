using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data.Action;
using XSheet.Util;
using System.Data;
using XSheet.Data;
using XSheet.v2.CfgBean;

namespace XSheet.v2.Data.XSheetRange
{
    public class XRangeRG:XRange
    {
        public int getLeftColumnIndex()
        {
            return getRange().LeftColumnIndex;
        }
        private DefinedName dname;
        public int getRightColumnIndex()
        {
            return getRange().RightColumnIndex; ;
        }

        public int getTopRowIndex()// 输入参数1，表示获取除标题行的首行
        {
            return getRange().TopRowIndex+1 ;
        }
        public int getBottomRowIndex()
        {
            return getRange().BottomRowIndex; ;
        }
        //1：参数区域在本区域内；2：参数区域在本区域内，但包含表头；-1参数区域不在本区域内
        //range为鼠标选择区域
        public override int isInRange(Range range)
        {
            return RangeUtil.isInRange(range, this.getRange());
        }

        public override void doResize(int rowcount)
        {
            Range range = getRange();
            //range.Fill.BackgroundColor = Color.White;
            
            dname.Range = RangeUtil.rangeResize(range,rowcount);
        }
        
        public override void fill(DataTable dt)
        {
            Range range = getRange();
            Cell data1stcell = get1stDataCell(range);
            string[,] arrtmp = new string[range.RowCount, range.ColumnCount];
            range.Worksheet.Import(arrtmp, data1stcell.RowIndex, data1stcell.ColumnIndex);
            range.Worksheet.Import(dt, false, data1stcell.RowIndex, data1stcell.ColumnIndex);
            data.setData(dt);
            //range.Borders.SetAllBorders(Color.Black, BorderLineStyle.None);
            /*for (int i = 0; i < range.RowCount; i++)
            {
                setRowBorderNone(i);
            }*/
            doResize(dt.Rows.Count);
        }
        public virtual Cell get1stDataCell(Range range)
        {
            return range[0, 0];
        }

        public override String getType()
        {
            return "Range";
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

        protected override void p_init()
        {
            throw new NotImplementedException();
        }

        public override void onSelect(Boolean isMutil)
        {
            throw new NotImplementedException();
        }

        protected override Boolean LocateRange(IWorkbook book)
        {
            if (book.DefinedNames.Contains(this.Name))
            {
                this.dname = book.DefinedNames.GetDefinedName(Name);
                return true;
            }
            return false;
        }

        public override Range getRange()
        {
            return getRange();
        }

        public override bool setRange(Range range)
        {
            dname.Range = range;
            return true;
        }

        public override List<string> getValiedLFunList()
        {
            return new List<string> { "R" };
        }

        public override void newData(int count)
        {
            int rowcount = getRange().RowCount + count;
            doResize(rowcount);
        }

        public override void doUpdate()
        {
            throw new NotImplementedException();
        }

        public override void doDelete()
        {
            throw new NotImplementedException();
        }

        public override void doInsert()
        {
            throw new NotImplementedException();
        }

        public override List<string> getSelectedValueByColIndex(int col)
        {
            throw new NotImplementedException();
        }

        public override void doSearch()
        {
            throw new NotImplementedException();
        }

        public override void doSearch(string sql)
        {
            throw new NotImplementedException();
        }

        public override void doUpdate(string sql)
        {
            throw new NotImplementedException();
        }

        public override void doInsert(string sql)
        {
            throw new NotImplementedException();
        }

        public override void doDelete(string sql)
        {
            throw new NotImplementedException();
        }

        internal override void onUpdateSelect(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
