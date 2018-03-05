using System;
using MySql.Data.MySqlClient;
using EpicReddit;

namespace EpicReddit.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            return new MySqlConnection(DBConfiguration.ConnectionString);
        }

        public static void Close(MySqlConnection conn)
        {
            conn.Close();
            if(conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
