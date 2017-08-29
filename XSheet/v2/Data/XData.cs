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
    /*存放实际datatable，并执行增删改查*/
    public class XData
    {
        private DataTable dt;//实际的数据对象
        public DbDataAdapter da { get; set; }//数据适配器，用于进行数据库操作
        public String ServerName { get; set; }//数据对象服务器名
        //public List<String> XSQLParams { get; set; }
        public String DBName { get; set; }//数据对象数据库名
        private DbConnection conn ;//数据库连接对象
        private DbTransaction DBTrans;//事务对象
        public List<String> avaliableList = new List<string>();//可用功能列表，初始化后自动生成 包含C、R、U、D
        //初始化，默认仅有读的功能
        public XData()
        {
            avaliableList = new List<string>();
            avaliableList.Add("R");
        }
        //查询功能，通过查询SQL可判定该对象具有的功能权限
        public String search(String Sql)
        {
            avaliableList.Clear();//清空功能列表
            try
            {
                conn = DBUtil.getConnection(ServerName);
                conn.Open();
                DBTrans = conn.BeginTransaction();
                conn.Close();
                da = DBUtil.getDbDataAdapter(ServerName, Sql, "", conn);//根据查询语句，初始化适配器
                DataSet ds = new DataSet();
                da.Fill(ds);
                dt = ds.Tables[0];
                avaliableList.Add("R");//根据适配器设定可用功能列表
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
            catch (Exception e)
            {
                AlertUtil.Show("err", e.ToString());
            }
           
            return "OK";
        } 
        //更新数据功能
        public String update()
        {
            String ans = "OK";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DBTrans = conn.BeginTransaction();
            da.UpdateCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
                ans = "OK"; 
            }
            catch (Exception e)
            {
                DBTrans.Rollback();
                Console.WriteLine(e.ToString());
                AlertUtil.Show("DataUpdateError", e.ToString());
                ans = "FAILED";
            }
            finally
            {
                conn.Close();
            }
            return ans;
        }
        //插入数据功能
        public String insert()
        {
            String ans = "OK";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DBTrans = conn.BeginTransaction();
            da.InsertCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
                ans = "OK";
            }
            catch (Exception e)
            {
                DBTrans.Rollback();
                Console.WriteLine(e.ToString());
                AlertUtil.Show("DataUpdateError", e.ToString());
                ans = "FAILED";
            }
            finally
            {
                conn.Close();
            }
            return ans;
        }
        //删除数据功能
        public String delete()
        {
            String ans = "OK";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DBTrans = conn.BeginTransaction();
            da.DeleteCommand.Transaction = DBTrans;
            try
            {
                da.Update(dt.GetChanges());
                dt.AcceptChanges();
                DBTrans.Commit();
                ans = "OK";
            }
            catch (Exception e)
            {
                DBTrans.Rollback();
                Console.WriteLine(e.ToString());
                AlertUtil.Show("DataUpdateError", e.ToString());
                ans = "FAILED";
            }
            finally
            {
                conn.Close();
            }
            return ans;
        }
        //筛选功能，在本地内存中筛选数据
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
        //对象异常检测，如果存放数据失败，则返回FALSE
        public Boolean isValied()
        {
            if (dt == null)
            {
                return false;
            }
            return true;
        }
        //手动存放/更新数据对象
        public void setData(DataTable dt)
        {
            this.dt = dt;
        }
        //获取数据对象
        public DataTable getDataTable()
        {
            return this.dt;
        }
        //初始化---未实现
        internal void init()
        {
            throw new NotImplementedException();
        }
        //刷新数据
        public void Refresh()
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
        //如果执行存储过程（执行存储过程不修改本地数据对象，可用来执行任意SQL语句）
        public String Execute(List<String> sqls)
        {
            String ans = "OK";
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DBTrans = conn.BeginTransaction();
            DbCommand cmd = DBUtil.getCommand(sqls[0], ServerName, conn);
            cmd.Transaction = DBTrans;
            try
            {
                foreach (String sql in sqls)
                {
                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();
                }
                DBTrans.Commit();
            }
            catch (Exception e)
            {
                AlertUtil.Show("error",e.ToString());
                ans = "FAILED";
                DBTrans.Rollback();
            }
            finally
            {
                conn.Close();
            }
            return ans;
        }
    }
}
