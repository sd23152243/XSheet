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
        public override void doCommand(String eventType,Range selectedRange)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override int isInRange(AreasCollection areas)
        {
            throw new NotImplementedException();
        }
    }
}
