
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.v2.Util
{
    class DBUtil
    {
        public static String getConnStr(String DBType)
        {
            String connStr = "";
            if (DBType.Contains(".xlsx"))
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + DBType + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=0\"";
            }
            else if (DBType.Contains(".xls"))
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DBType + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=0\"";
            }
            else
            {
                try
                {
                    connStr = ConfigUtil.GetConnectionString(DBType.ToUpper());
                    String privoder = ConfigUtil.GetProviderName(DBType.ToUpper());
                    if (privoder != "System.Data.SqlClient")
                    {
                        connStr = "Provider="+privoder +";"+ ConfigUtil.GetConnectionString(DBType.ToUpper());
                    }
                    //System.Windows.Forms.MessageBox.Show(connStr);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("链接类型："+DBType+"不存在");
                    Console.WriteLine(e.ToString());
                }
              
            }
            return connStr;
        }
        public static DataTable getDataTable(String DBType, String Sql, String sqlType, String param,DbConnection conn)
        {
            if (conn == null)
            {
                conn = getConnection(DBType);
            }
            DataTable table = new DataTable();
            DbDataAdapter da = null;
            if (ConfigUtil.GetProviderName(DBType.ToUpper()) == "System.Data.SqlClient")
            {
                da = DBUtil.getSqlDataAdapter(conn, Sql, sqlType);
            }
            else{
                da = DBUtil.getOleDbDataAdapter(conn, Sql, sqlType);
            }
            if (da != null)
            {
                da.Fill(table);
            }
            else
            {
                table = null;
            }
            
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBType">服务器</param>
        /// <param name="Sql">Sql语句</param>
        /// <param name="sqlType">PROCEDURE时为存储过程,其他时候为SQL语句</param>
        /// <returns></returns>
        public static DbDataAdapter getDbDataAdapter(String DBType, String Sql, String sqlType, DbConnection dbConn)
        {
            DbDataAdapter da = null;
            try
            {
                switch (DBType.ToUpper())
                {
                    case "AS400":
                        da =  getOleDbDataAdapter(dbConn, Sql, sqlType);
                        break;
                    case "SRF-SQL":
                    case "ICHART3D":
                        da =  getSqlDataAdapter(dbConn, Sql, sqlType);
                        break;
                    default:
                        da = getOleDbDataAdapter(dbConn, Sql, sqlType);
                        break;
                }
            }
            catch (Exception e)
            {

                AlertUtil.Show("error",e.ToString());
            }
            return da;
        }

        /*public static iDB2DataAdapter getiDB2DataAdapter(String DBType, String Sql, String sqlType)
        {
            iDB2DataAdapter da;
            iDB2Connection dbConn = new iDB2Connection();
            dbConn.ConnectionString = getConnStr(DBType);
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new iDB2DataAdapter(Sql, dbConn);
            if (sqlType.ToUpper() == "PROCEDURE")
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                iDB2CommandBuilder builder = new iDB2CommandBuilder(da);
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
            }
            return da;
        }
        */
        public static OleDbDataAdapter getOleDbDataAdapter(DbConnection Conn, String Sql, String SqlType)
        {
            OleDbDataAdapter da;
            OleDbConnection dbConn = (OleDbConnection)Conn;
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != ConnectionState.Open)
                dbConn.Open();
            da = new OleDbDataAdapter(Sql, dbConn);
            if (SqlType.ToUpper() == "PROCEDURE")
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                
                try
                {
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(da);
                    da.DeleteCommand = builder.GetDeleteCommand();
                    da.UpdateCommand = builder.GetUpdateCommand();
                    da.InsertCommand = builder.GetInsertCommand();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            return da;
        }

        public static SqlDataAdapter getSqlDataAdapter(DbConnection Conn, String Sql,String SqlType)
        {
            SqlDataAdapter da;
            SqlConnection dbConn = (SqlConnection)Conn;
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new SqlDataAdapter(Sql, dbConn);
            if (SqlType.ToUpper() == "PROCEDURE")
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
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
            }
            dbConn.Close();
            return da;
        }

        public static DbConnection getConnection(String DBType)
        {
            DbConnection dbConn = null;
            switch (DBType.ToUpper())
            {
                case "AS400":
                    dbConn = new OleDbConnection();
                    break;
                case "SRF-SQL":
                case "ICHART3D":
                case "MAIN":
                    dbConn = new SqlConnection();
                    break;
                default:
                    dbConn = new OleDbConnection();
                    break;
            }
            dbConn.ConnectionString = getConnStr(DBType);
            return dbConn;
        }

        public static DbCommand getCommand(String sql,String DBType, DbConnection conn)
        {
            DbCommand cmd = null;
            switch (DBType.ToUpper())
            {
                case "AS400":
                    cmd = new OleDbCommand(sql,(OleDbConnection)conn);
                    break;
                case "SRF-SQL":
                case "ICHART3D":
                    cmd = new SqlCommand(sql, (SqlConnection)conn);
                    break;
                default:
                    cmd = new OleDbCommand(sql, (OleDbConnection)conn);
                    break;
            }
            return cmd;
        }
    }
}
