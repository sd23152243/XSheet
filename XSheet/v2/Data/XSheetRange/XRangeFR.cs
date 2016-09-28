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
        private DefinedName dname;
        public override void doResize(int rowcount)
        {
            return;
        }

        public override int isInRange(Range range)
        {
            AreasCollection areas = getRange().Areas;
            foreach (Range irange in areas)
            { 
                if (RangeUtil.isInRange(range,irange) > 0)
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
            return;
        }

        public override void onSelect(Boolean isMutil)
        {
            return;
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
            return dname.Range;
        }

        public override bool setRange(Range range)
        {
            dname.Range = range;
            return true;
        }

        public override List<string> getValiedLFunList()
        {
            List<string> list = new List<string>();
            List<string> cmdFunc = getCommandFunList();
            foreach (char item in cfg.CRUDP)
            {
                if (data.avaliableList.Contains(item.ToString()) || item == 'P')
                {
                    list.Add(item.ToString());
                }
            }
           
            if (getCommandByEvent(SysEvent.Btn_Search) != null && getCommandByEvent(SysEvent.Btn_Search).Count > 1)
            {
                list.Add("RO");
            }
            if (getCommandByEvent(SysEvent.Btn_Exe) != null)
            {

                if (getCommandByEvent(SysEvent.Btn_Exe).Count > 1)
                {
                    list.Add("PM");
                }
            }
            else
            {
                list.Remove("P");
            }
            return list;
        }

        private List<string> getCommandFunList()
        {
            List<String> list = new List<string>();
            foreach (SysEvent e in commands.Keys)
            {
                switch (e)
                {
                    case SysEvent.Sheet_Init:
                        break;
                    case SysEvent.Sheet_Change:
                        break;
                    case SysEvent.Cell_Change:
                        break;
                    case SysEvent.Btn_Search:
                        list.Add("R");
                        break;
                    case SysEvent.Btn_Edit:
                        list.Add("U");
                        break;
                    case SysEvent.Btn_Delete:
                        list.Add("D");
                        break;
                    case SysEvent.Btn_New:
                        list.Add("C");
                        break;
                    case SysEvent.Btn_Exe:
                        list.Add("P");
                        break;
                    case SysEvent.Key_Enter:
                        break;
                    case SysEvent.Select_Change:
                        break;
                    case SysEvent.Btn_Cancel:
                        break;
                    case SysEvent.Btn_Save:
                        break;
                    case SysEvent.Event_Search:
                        break;
                    default:
                        break;
                }
            }
            return list;
        }

        public override void newData(int count)
        {
            foreach (Range range in getRange().Areas)
            {
                range.Value = "";
            }
        }

        public override String doUpdate()
        {
            DataTable dt = data.getDataTable();
            DataRow row = dt.Rows[0];
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strRange = getRange().Areas[j].Value.ToString();
                if (row[j].ToString() == strRange)
                {
                    continue;
                }
                Type t = row[j].GetType();

                if (t.Name == "Decimal")
                {
                    Decimal num = Convert.ToDecimal(strRange);
                    row[j] = (object)num;
                }
                else
                {
                    row[j] = strRange;
                }
            }
            data.setData(dt);
            return data.update();
        }

        public override String doDelete()
        {
            DataTable dt = data.getDataTable();
            dt.Rows[0].Delete();
            data.setData(dt);
            return data.delete();
        }

        public override String doInsert()
        {
            DataTable dt = data.getDataTable();
            int dcount = getDataTable().Rows.Count;
            int maxcount = getRange().RowCount;
            DataRow templet = dt.Rows[0];
            DataRow row = dt.NewRow();
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strRange = getRange().Areas[j].Value.ToString();
                Type t = templet[j].GetType();

                if (t.Name == "Decimal")
                {
                    Decimal num = Convert.ToDecimal(strRange);
                    row[j] = (object)num;
                }
                else
                {
                    row[j] = strRange;
                }

            }
            dt.Rows.Add(row);
            data.setData(dt);
            return data.insert();
        }

        public override List<string> getSelectedValueByColIndex(int col)
        {
            List<string> ans = new List<string>();
            try
            {
                ans.Add(getRange().Areas[col][0].ToString());
            }
            catch (Exception)
            {
                ans = null;
                throw;
            }
            return ans;
        }

        public override void onUpdateSelect()
        {
            return;
        }

        public override string doSearch(List<string> sqls)
        {
            String sql = sqls[0];
            for (int i = 1; i < sqls.Count; i++)
            {
                sql = sql + " UNION " + sqls[i];
            }
            String ans = data.search(sql);
            fill(data.getDataTable());
            return ans;
        }

        public override string ExecuteSql(List<string> sqls)
        {
            return data.Execute(sqls);
        }

        public override void ResetSelected()
        {
            return;
        }
    }
}
