using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Data
{
    public class XData
    {
        public String BaseStatement { get; set; }
        private DataTable dt;
        public IDataAdapter da { get; set; }
        public String serverName { get; set; }
        public String XSQL { get; set; }
        public List<String> XSQLParams { get; set; }
        private Boolean autoable { get; set; }
        public void search()
        {

        } 

        public void delete()
        {
            
        }

        public void update()
        {

        }

        public void insert()
        {

        }


        public DataTable select(string filterExpression)
        {
            DataRow[] rows =  dt.Select(filterExpression);
            DataTable ndt = dt.Clone();
            foreach (DataRow row in rows)
            {
                ndt.ImportRow(row);
            }
            return ndt;
        }

        public Boolean isValied()
        {
            if (dt == null)
            {
                return false;
            }
            return true;
        }

        public void setData(DataTable dt)
        {
            this.dt = dt;
        }

        public DataTable getData()
        {
            return this.dt;
        }
    }
}
