using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using XSheet.Data;
using XSheet.Util;
using XSheet.v2.CfgBean;

namespace XSheet.v2.Data
{ 
    public abstract class XRange
    {
        protected DefinedName dname;//实际sheet中的DefinedName
        public String Name { get; set; }//DefinedName 名称
        public XRSheet sheet { get; set; }//DefinedName 所在的位置
        public Dictionary<String, XCommand> commands { get; set; }//Name中所绑定的Command，String为事件，XCommand为事件对应命令
        public String type { get; set; }//Xname所对应的类型，Table/Range/Form
        public DataCfg cfg { get; set; }//从配置Sheet中读取的配置信息
        protected XData data { get; set; }//当前Range中所包含的
        //public DbDataAdapter da { get; set; }
        public abstract void init(DataCfg cfg);
        public Dictionary<String, XAction> actions { get; set; }
        public virtual Range getRange()
        {
            try
            {
                return dname.Range;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return null;
            }
            
        }
        abstract public int isInRange(Range range);
        abstract public String getType();
        abstract public Boolean isSelectable();
        abstract public void onSelect();
        public int isInRange(AreasCollection areas)
        {
            int i = 0;
            foreach (Range range in areas)
            {
                i = isInRange(range);
                if (i < 0)
                {
                    break;
                }
            }
            return i;
        }
        abstract public void doCommand(String eventType);
        abstract public void doResize(int rowcount, int columncount);
        public XRange()
        {
            commands = new Dictionary<string, XCommand>();
            data = new XData();
        }

        public virtual String getSqlStatement()
        {
            /*if (this.cfg.sqlStatement.StartsWith("*"))
            {
                String r1a1 = cfg.sqlStatement.Remove(0, 1);
                Console.WriteLine(r1a1);
                Range range = getRange().Worksheet.Workbook.Range[r1a1];
                return range[0, 0].DisplayText;
            }
            return cfg.sqlStatement;*/ //TODO
            return null;
            
        }
        public abstract void fill(DataTable dt);
        public abstract void fill(String sql);
        public abstract void fill();
        public virtual DataTable getData()
        {
            return data.getData();
        }

        public virtual Boolean isDataValied()
        {
            return data.isValied();
        }

        public DbDataAdapter getDbDataAdapter(String sql)
        {
            return DBUtil.getDbDataAdapter(cfg.ServerName, sql, "");
        }
    }
}
