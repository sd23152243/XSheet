using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Spreadsheet;
using XSheet.Data.Action;
using XSheet.Util;

namespace XSheet.Data
{
    public class XNamedForm:XNamed
    {
        public override void doCommand(String eventType)
        {
            XCommand command = null;
            try
            {
                command = commands[eventType.ToUpper()];
                command.execute();
            }
            catch (Exception e)
            {
                //System.Windows.Forms.MessageBox.Show("事件"+eventType+"未绑定命令");
                Console.WriteLine(e.ToString());
                return;
            }
        }

        public override void doResize(int rowcount, int columncount)
        {
            return ;
        }

        public override int isInRange(Range range)
        {
            AreasCollection areas = getRange().Areas;
            
            foreach (Range irange in areas)
            { 
                if (RangeUtil.isInRange(irange,getRange()) > 0)
                {
                    return 1;
                }
            }
            return -1;
        }

        public override void fill(DataTable dt)
        {
            Range ranges = getRange();
            for (int i = 0; i < ranges.Areas.Count; i++)
            {
                Range range = ranges.Areas[i];
                range.Value = dt.Rows[0][i].ToString();
            }
        }

        /*public override int isInRange(AreasCollection areas)
        {
            throw new NotImplementedException();
        }*/
    }
}
