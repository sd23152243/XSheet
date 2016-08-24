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
    public class RangeUtil
    {
        //判断某个区域是否在命名区域中
        public static XRange getNamedByRange(Range range, List<XRangeForm> names)
        {
            foreach (XRangeForm name in names)
            {
                if (name.isInRange(range) >0)
                {
                    return name;
                }
            }
            return null;
        }
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
    }
}
