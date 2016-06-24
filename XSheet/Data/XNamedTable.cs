using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Data.Action;
using XSheet.Util;

namespace XSheet.Data
{
    public class XNamedTable:XNamedRange
    {
        private Table table;
        public override void setDefinedName(Worksheet sheet)
        {
            
            foreach (Table table in sheet.Tables)
            {
                if (table.Name.ToUpper().Trim() == this.Name.ToUpper().Trim())
                {
                    this.table = table;
                    break;
                }
            }
            table.Range = getRange();
        }

        public override Range getRange()
         {
            return this.table.Range;
        }

        public override void doCommand(string eventType,AreasCollection selectedRange)
        {
            XCommand command = commands[eventType.ToUpper()];
            command.execute(selectedRange); 
        }

        public override void changeDefinedRange(Range newrange)
        {
            /*int topRowIndex = newrange.TopRowIndex;
            int bottomRowIndex = newrange.BottomRowIndex;
            int leftColumnIndex = newrange.LeftColumnIndex;
            int rightColumnIndex = newrange.RightColumnIndex;*/
            this.table.Range = newrange;//.Worksheet.Range.FromLTRB(leftColumnIndex, topRowIndex, rightColumnIndex, bottomRowIndex);
        }
        public override Cell get1stDataCell(Range range)
        {
            return range[1, 0];
        }

        public override int getIndexAddedDataCount(int dataCount)
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
            rowIndex = rowIndex + 1;
            return rowIndex;
        }
    }
}
