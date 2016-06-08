using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Util
{
    class DBUtil
    {
        public static String getConnStr(String DBType)
        {
            String connStr;
            switch (DBType.ToUpper())
            {
                case "AS400":
                    connStr = "Provider=IBMDA400;Data Source=172.31.72.37;User Id=ITSDTS;Password = STD008;";
                    break;
                case "SRF-SQL":
                    connStr = "Data Source=srf-sql;User Id=MARS_E;Password = rs@996t!ty";
                    break;
                case "ICHART3D":
                    connStr = "Data Source=ichart3d;User Id=MARS_E;Password = rs@996t!ty";
                    break;
                default:
                    connStr = "UNKNOWN DEVICE";
                    break;
            }
            return connStr;
        }
        public static DataTable getDataTable(String DBType, String Sql)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            switch (DBType)
            {
                case "AS400":
                    DBUtil.getOleDbDataAdapter(DBType, Sql).Fill(ds);
                    table = ds.Tables[0];
                    break;
                case "SRF-SQL":
                case "ICHART3D":
                    DBUtil.getSqlDataAdapter(DBType, Sql).Fill(ds);
                    table = ds.Tables[0];
                    break;
                default:
                    table = null;
                    break;
            }
            return table;
        }

        private static OleDbDataAdapter getOleDbDataAdapter(String DBType, String Sql)
        {
            OleDbDataAdapter da;
            OleDbConnection dbConn = new OleDbConnection();
            dbConn.ConnectionString = getConnStr(DBType);
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new OleDbDataAdapter(Sql, dbConn);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
            try
            {
                da.DeleteCommand = builder.GetDeleteCommand();
                da.UpdateCommand = builder.GetUpdateCommand();
                da.InsertCommand = builder.GetInsertCommand();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return da;
        }

        public static SqlDataAdapter getSqlDataAdapter(String DBType, String Sql)
        {
            SqlDataAdapter da;
            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = getConnStr(DBType);
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new SqlDataAdapter(Sql, dbConn);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            try
            {
                da.DeleteCommand = builder.GetDeleteCommand();
                da.UpdateCommand = builder.GetUpdateCommand();
                da.InsertCommand = builder.GetInsertCommand();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }
            return da;
        }
    }
}
