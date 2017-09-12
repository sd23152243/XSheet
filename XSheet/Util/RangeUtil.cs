using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.v2.Data;
using XSheet.v2.Data.XSheetRange;

namespace XSheet.Util
{
    /*区域工具类*/
    public class RangeUtil
    {
        //判断某个区域是否在命名区域中
        public static XRange getNamedByRange(Range range, List<XRangeFR> names)
        {
            foreach (XRangeFR name in names)
            {
                if (name.isInRange(range) >0)
                {
                    return name;
                }
            }
            return null;
        }
        /*根据区域名称、文档获取全局区域*/
        public static Range getRangeByName(String name,IWorkbook book)
        {
            Range range = null;
            try
            {
                range = book.Range[name];
            }
            catch (Exception)
            {
                
            }
            return range;
        }
        /*根据区域名称、Sheet获取Sheet内区域*/
        public static Range getRangeByName(String name, Worksheet sheet)
        {
            Range range = null;
            try
            {
                range = sheet[name];
            }
            catch (Exception)
            {

            }
            return range;
        }
        //判断是否在所选area是否在range内，0表示正常 <0异常
        public static int isInRange(AreasCollection areas,Range range)
        {
            int i = 0;
            foreach (Range tmprange in areas)
            {
                i = isInRange(tmprange,range);
                if (i < 0)
                {
                    break;
                }
            }
            return i;
        }
        
        public static int isInRange(Range x,Range range)//判断x是否在range中
        {
            int result = 0;
            int flag = 1;
            if (x.TopRowIndex < range.TopRowIndex)
            {
                result = result + 1;
                flag = -1;
            }
            if (x.RightColumnIndex > range.RightColumnIndex)
            {
                result = result + 2;
                flag = -1;
            }
            if (x.LeftColumnIndex < range.LeftColumnIndex)
            {
                result = result + 4;
                flag = -1;
            }

            if (x.BottomRowIndex > range.BottomRowIndex)
            {
                result = result + 8;
                flag = -1;

            }
            if (x.TopRowIndex == range.TopRowIndex)
            {
                result = result + 16;
            }
            return result * flag;
        }

        public static void fillRangeBackgroud(Range range,Color color)
        {
            Formatting rangeFormatting = range.BeginUpdateFormatting();

            //Specify font appearance (font name, color, size and style).
            rangeFormatting.Fill.BackgroundColor = color;

            //Complete updating range formatting.
            range.EndUpdateFormatting(rangeFormatting);
        }

        //重定义RANGE大小
        public static Range rangeResize(Range range,int rowcount)
        {
            //range.Fill.BackgroundColor = Color.White;
            String rfA1 = range.GetReferenceA1(ReferenceElement.ColumnAbsolute | ReferenceElement.RowAbsolute);

            String[] tmp = rfA1.Split('$');
            if (tmp.Length > 5)
            {
                System.Windows.Forms.MessageBox.Show("当前区域:" + rfA1 + "定义不规范 定义应为$A$1:$C$10");
                return range;
            }
            else if (tmp.Length == 3)
            {
                rfA1 = rfA1 + ":" + rfA1;
                tmp = rfA1.Split('$');
            }


            int rowIndex = getIndexAddedDataCount(range,rowcount);
            tmp[tmp.Length - 1] = rowIndex.ToString();
            Range newrange = range.Worksheet.Range[string.Join("$", tmp)];
            return newrange;
        }
        //获取序列索引
        private static int getIndexAddedDataCount(Range range ,int dataCount)
        {
            int rowIndex = range.TopRowIndex;
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
    }
}
