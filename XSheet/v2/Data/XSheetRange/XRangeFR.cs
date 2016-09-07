using System;
using System.Data;
using DevExpress.Spreadsheet;
using XSheet.Util;
using XSheet.Data;
using XSheet.v2.CfgBean;
using System.Collections.Generic;

namespace XSheet.v2.Data.XSheetRange
{
    public class XRangeFR:XRange
    {
        public override void doResize(int rowcount)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public override Range getRange()
        {
            throw new NotImplementedException();
        }

        public override bool setRange(Range range)
        {
            throw new NotImplementedException();
        }

        public override List<string> getValiedLFunList()
        {
            throw new NotImplementedException();
        }

        public override void newData(int count)
        {
            throw new NotImplementedException();
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
    }
}
