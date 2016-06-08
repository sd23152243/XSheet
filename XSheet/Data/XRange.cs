using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Data
{
    class XRange
    {
        Range range { get; set; }
        public int getLeftColumnIndex()
        {
            return range.LeftColumnIndex;
        }

        public int getRightColumnIndex()
        {
            return range.RightColumnIndex; ;
        }

        public int getTopRowIndex()
        {
            return range.TopRowIndex; ;
        }
        public int getTopRowIndex(int i)// 输入参数1，表示获取除标题行的首行
        {
            if (i ==1)
            {
                return range.TopRowIndex - 1;
            }
            return getTopRowIndex();
        }

        public int getBottomRowIndex()
        {
            return range.BottomRowIndex; ;
        }

        public int isInRange(XRange x)//1：参数区域在本区域内；2：参数区域在本区域内，但包含表头；-1参数区域不在本区域内
        {
            Range xrange = x.range;
            int result = 0;
            int flag = 1;
            if (xrange.TopRowIndex > range.TopRowIndex)
            {
                result = result + 1;
                flag = -1;
            }
            if (xrange.RightColumnIndex > range.LeftColumnIndex)
            {
                result = result + 2;
                flag = -1;
            }
            if (xrange.LeftColumnIndex < range.LeftColumnIndex)
            {
                result = result + 4;
                flag = -1;
            }
            
            if(xrange.BottomRowIndex>range.BottomRowIndex)
            {
                result = result + 8;
                flag = -1;
                
            }
            if (xrange.TopRowIndex == range.TopRowIndex)
            {
                result = result + 16;
            }
            return result*flag;
        }
    }
}
