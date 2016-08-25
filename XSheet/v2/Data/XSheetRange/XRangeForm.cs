using System;
using System.Data;
using DevExpress.Spreadsheet;
using XSheet.Util;
using XSheet.Data;
using XSheet.v2.CfgBean;

namespace XSheet.v2.Data.XSheetRange
{
    public class XRangeForm:XRange
    {
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

        public override string getType()
        {
            return "Form";
        }
        public override bool isSelectable()
        {
            return false;
        }

        public override void init(DataCfg cfg)
        {
            throw new NotImplementedException();
        }
        public override void onSelect()
        {
            throw new NotImplementedException();
        }

        public override void fill(string sql)
        {
            throw new NotImplementedException();
        }

        public override void fill()
        {
            throw new NotImplementedException();
        }
    }
}
