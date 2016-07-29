﻿using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data.Action;
using XSheet.Util;
using System.Data;

namespace XSheet.Data
{
    public class XNamedRange:XNamed
    {
        public int getLeftColumnIndex()
        {
            return getRange().LeftColumnIndex;
        }

        public int getRightColumnIndex()
        {
            return getRange().RightColumnIndex; ;
        }

        public int getTopRowIndex()
        {
            return getRange().TopRowIndex; ;
        }
        public int getTopRowIndex(int i)// 输入参数1，表示获取除标题行的首行
        {
            if (i == 1)
            {
                return getRange().TopRowIndex + 1;
            }
            return getTopRowIndex();
        }

        public int getBottomRowIndex()
        {
            return getRange().BottomRowIndex; ;
        }
        
        public override int isInRange(Range x)//1：参数区域在本区域内；2：参数区域在本区域内，但包含表头；-1参数区域不在本区域内
        //x为鼠标选择区域
        {
            return RangeUtil.isInRange(x,this.getRange());
        }

        public override void doCommand(string eventType)
        {
            XCommand command = null;
            try
            {
                command = commands[eventType.ToUpper()];
                command.execute();
            }
            catch (Exception)
            {
                //System.Windows.Forms.MessageBox.Show("事件"+eventType+"未绑定命令");
                return;
            }
            
        }

        public override void doResize(int rowcount, int columncount)
        {
            Range range = getRange();
            //range.Fill.BackgroundColor = Color.White;
            String rfA1 = range.GetReferenceA1(ReferenceElement.ColumnAbsolute | ReferenceElement.RowAbsolute);
            
            String[] tmp = rfA1.Split('$');
            if (tmp.Length>5)
            {
                System.Windows.Forms.MessageBox.Show("当前区域:"+Name+"定义不规范，当前定义类型为"+ rfA1 + "Range类型定义应为$A$1:$C$10");
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
        public virtual int getIndexAddedDataCount(int dataCount)
        {
            int rowIndex = getRange().TopRowIndex;
            if (dataCount == 0)
            {
                rowIndex++;
            }
            else
            {
                rowIndex += dataCount;
            }
            return rowIndex;
        }


        public virtual void changeDefinedRange(Range newrange)
        {
            getRange().ClearFormats();
            this.dname.Range = newrange;
            RangeUtil.fillRangeBackgroud(getRange(), Color.CadetBlue);
            for (int i = 0; i <= getRange().RowCount; i++)
            {
                getRange()[i, 0].Tag = i;
            }

        }

        public override void fill(DataTable dt)
        {
            selectedRows = new Dictionary<int, int>();
            Range range = getRange();
            Cell data1stcell = get1stDataCell(range);
            string[,] arrtmp = new string[range.RowCount, range.ColumnCount];
            range.Worksheet.Import(arrtmp, data1stcell.RowIndex, data1stcell.ColumnIndex);
            range.Worksheet.Import(dt, false, data1stcell.RowIndex, data1stcell.ColumnIndex);
            this.dt = dt;
            for (int i = 0; i < range.RowCount; i++)
            {
                setRowBorderNone(i);
            }
            doResize(dt.Rows.Count, dt.Columns.Count);
            
        }
        public virtual Cell get1stDataCell(Range range)
        {
            return range[0, 0];
        }

        /*public override int isInRange(AreasCollection areas)
        {
            int i = 0;
            foreach (Range range in areas)
            {
                i = isInRange(range);
                if (i<0)
                {
                    break;
                }
            }
            return i;
        }*/
    }
}
