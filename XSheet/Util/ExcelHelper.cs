using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSheet.Util
{
    class ExcelHelper
    {
        private static string fileName = "//ichart3d/XSheetModel/AAA.xlsx";

        private static OleDbConnection connection;
        public static OleDbConnection Connection
        {
            get
            {
                string connectionString = "";
                string fileType = System.IO.Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(fileType)) return null;
                if (fileType == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                }
                else
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                }
                if (connection == null)
                {
                    connection = new OleDbConnection(connectionString);
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                else if (connection.State == System.Data.ConnectionState.Broken)
                {
                    connection.Close();
                    connection.Open();
                }
                return connection;
            }
        }

        /// <summary>  
        /// 执行无参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回受SQL语句影响的行数</returns>  
        public static int ExecuteCommand(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        /// <summary>  
        /// 执行有参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="values">参数集合</param>  
        /// <returns>返回受SQL语句影响的行数</returns>  
        public static int ExecuteCommand(string sql, params OleDbParameter[] values)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(values);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        /// <summary>  
        /// 返回单个值无参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回受SQL语句查询的行数</returns>  
        public static int GetScalar(string sql)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return result;
        }

        /// <summary>  
        /// 返回单个值有参数的SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="parameters">参数集合</param>  
        /// <returns>返回受SQL语句查询的行数</returns>  
        public static int GetScalar(string sql, params OleDbParameter[] parameters)
        {
            OleDbCommand cmd = new OleDbCommand(sql, Connection);
            cmd.Parameters.AddRange(parameters);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            connection.Close();
            return result;
        }

        /// <summary>  
        /// 执行查询无参数SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <returns>返回数据集</returns>  
        public static DataTable GetReader(string sql)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            connection.Close();
            return dt;
        }

        /// <summary>  
        /// 执行查询有参数SQL语句  
        /// </summary>  
        /// <param name="sql">SQL语句</param>  
        /// <param name="parameters">参数集合</param>  
        /// <returns>返回数据集</returns>  
        public static DataSet GetReader(string sql, params OleDbParameter[] parameters)
        {
            OleDbDataAdapter da = new OleDbDataAdapter(sql, Connection);
            da.SelectCommand.Parameters.AddRange(parameters);
            DataSet ds = new DataSet();
            da.Fill(ds);
            connection.Close();
            return ds;
        }
    }
}
