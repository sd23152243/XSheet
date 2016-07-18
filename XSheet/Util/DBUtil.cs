
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
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
                    //Provider=IBMDA400;
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
        public static DataTable getDataTable(String DBType, String Sql, String sqlType, String param)
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();
            DbDataAdapter da = null;
            switch (DBType.ToUpper())
            {
                case "AS400":
                    da = DBUtil.getOleDbDataAdapter(DBType, Sql, sqlType);
                    if (param != null && param.Length > 0)
                    {
                        String[] paramList = param.Split(new char[3] { '#', '_', '#' });
                        foreach (String strparam in paramList)
                        {
                            OleDbParameter sqlparam = new OleDbParameter("@p1", OleDbType.Char, 10);
                            sqlparam.Direction = ParameterDirection.ReturnValue;
                            sqlparam.Value = strparam;
                            da.SelectCommand.Parameters.Add(sqlparam);
                        }
                    }

                    break;
                case "SRF-SQL":
                case "ICHART3D":
                    da = DBUtil.getSqlDataAdapter(DBType, Sql, sqlType);
                    if (param != null && param.Length > 0)
                    {
                        String[] paramList = param.Split(new char[3] { '#', '_', '#' });
                        foreach (String strparam in paramList)
                        {
                            SqlParameter sqlparam = new SqlParameter("@p1", SqlDbType.Char);
                            sqlparam.Direction = ParameterDirection.ReturnValue;
                            sqlparam.Value = strparam;
                            da.SelectCommand.Parameters.Add(sqlparam);
                        }
                    }
                    break;
                default:
                    break;
            }
            if (da != null)
            {
                da.Fill(ds);
                table = ds.Tables[0];
            }
            else
            {
                table = null;
            }

            return table;
        }



        public static DbDataAdapter getDbDataAdapter(String DBType, String Sql, String sqlType)
        {
            switch (DBType.ToUpper())
            {
                case "AS400":
                    return getOleDbDataAdapter(DBType, Sql, sqlType);
                case "SRF-SQL":
                case "ICHART3D":
                    return getSqlDataAdapter(DBType, Sql, sqlType);
                default:
                    return getOleDbDataAdapter(DBType, Sql, sqlType);
            }
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
        public static OleDbDataAdapter getOleDbDataAdapter(String DBType, String Sql, String sqlType)
        {
            OleDbDataAdapter da;
            OleDbConnection dbConn = new OleDbConnection();
            dbConn.ConnectionString = getConnStr(DBType);
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new OleDbDataAdapter(Sql, dbConn);
            if (sqlType.ToUpper() == "PROCEDURE")
            {
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
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
            }

            return da;
        }

        public static SqlDataAdapter getSqlDataAdapter(String DBType, String Sql, String sqlType)
        {
            SqlDataAdapter da;
            SqlConnection dbConn = new SqlConnection();
            dbConn.ConnectionString = getConnStr(DBType);
            //dbConn.ConnectionString = "Provider=IBMDA400;Database=R21AFLBZ;Hostname=172.31.71.37;Uid=ITSDTS;Pwd=STD008;";
            if (dbConn.State != System.Data.ConnectionState.Open)
                dbConn.Open();
            da = new SqlDataAdapter(Sql, dbConn);
            if (sqlType.ToUpper() == "PROCEDURE")
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
            return da;
        }
    }
}
