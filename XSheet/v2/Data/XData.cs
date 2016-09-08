using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSheet.Util;
using XSheet.v2.Util;

namespace XSheet.v2.Data
{
    public class XData
    {
        private DataTable dt;
        public DbDataAdapter da { get; set; }
        public String ServerName { get; set; }
        //public List<String> XSQLParams { get; set; }
        public String DBName { get; set; }
        private DbConnection conn ;
        private DbTransaction DBTrans;
        public List<String> avaliableList = new List<string>();
        public XData()
        {
            avaliableList = new List<string>();
            avaliableList.Add("R");
        }
        public void search(String Sql)
        {
            avaliableList.Clear();
            conn = DBUtil.getConnection(ServerName);
            conn.Open();
            DBTrans = conn.BeginTransaction();
            conn.Close();
            da = DBUtil.getDbDataAdapter(ServerName, Sql, "",conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            avaliableList.Add("R");
            if (da.UpdateCommand != null)
            {
                avaliableList.Add("U");
            }
            if (da.InsertCommand != null)
            {
                avaliableList.Add("C");
            }
            if (da.DeleteCommand != null)
            {
                avaliableList.Add("D");
            }
        } 

        public void delete()
        {
            conn.Open();
            da.DeleteCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
            }
            catch (Exception e)
            {

                AlertUtil.Show("error", e.ToString());
            }
            conn.Close();
        }

        public void update()
        {
            da.UpdateCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
            }
            catch (Exception e)
            {

                AlertUtil.Show("error", e.ToString());
            }
        }

        public void insert()
        {
            da.InsertCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
            }
            catch (Exception e)
            {

                AlertUtil.Show("error", e.ToString());
            }
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

        public DataTable getDataTable()
        {
            return this.dt;
        }

        internal void init()
        {
            throw new NotImplementedException();
        }

        internal void Refresh()
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            avaliableList.Add("R");
            if (da.UpdateCommand != null)
            {
                avaliableList.Add("U");
            }
            if (da.InsertCommand != null)
            {
                avaliableList.Add("C");
            }
            if (da.DeleteCommand != null)
            {
                avaliableList.Add("D");
            }
        }
    }
}
