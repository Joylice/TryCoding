#define Test
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace Cactus.Common
{
    public class SqlString
    {

        public static string MSSQLString = ConfigurationManager.ConnectionStrings["MSSQLString"].ConnectionString;
        public static string MySQLString = ConfigurationManager.ConnectionStrings["MySQLString"].ConnectionString;
        public static string SQLiteString = "Data Source=" + AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.ConnectionStrings["SQLiteString"].ConnectionString;

        public static IDbConnection GetSqlConnection(string sqlConnectionString = "")
        {
            if (sqlConnectionString == "") {
                sqlConnectionString = MSSQLString;
            }
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
        public static IDbConnection GetMySqlConnection(string sqlConnectionString = "")
        {
            if (sqlConnectionString == "")
            {
                sqlConnectionString = MySQLString;
            }
            MySqlConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }

        public static  IDbConnection GetSQLiteConnection(string sqlConnectionString = ""){
            if (sqlConnectionString == "")
            {
                sqlConnectionString = SQLiteString;
            }
            SQLiteConnection conn = new SQLiteConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
