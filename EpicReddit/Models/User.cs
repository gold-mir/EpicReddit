using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EpicReddit.Models
{
    public class ERUser
    {
        private int _id;
        private string _username;

        private ERUser(string username, int id)
        {
            _username = username;
            _id = id;
        }
        
        public static ERUser Create(string username, string password)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"INSERT INTO users (username, password) VALUES (@Username, @Password);";

            MySqlParameter newUsername = new MySqlParameter();
            newUsername.ParameterName = "@Username";
            newUsername.Value = username;
            cmd.Parameters.Add(newUsername);

            MySqlParameter newPassword = new MySqlParameter();
            newPassword.ParameterName = "@Password";
            newPassword.Value = password;
            cmd.Parameters.Add(newPassword);

            cmd.ExecuteNonQuery();

            int id = (int)cmd.LastInsertedId;

            DB.Close(conn);

            return new ERUser(username, id);
        }

        public int GetID()
        {
            return _id;
        }

        public string GetUsername()
        {
            return _username;
        }

        public bool ValidatePassword(string password)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT password FROM users WHERE id = {_id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            string dbPassword = "";
            while(rdr.Read())
            {
                dbPassword = rdr.GetString(0);
            }

            DB.Close(conn);

            return dbPassword == password;
        }

        public static bool ValidateUserInfo(string username, string password)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT COUNT(id) FROM users WHERE username = @Username AND password = @Password";

            MySqlParameter newUsername = new MySqlParameter();
            newUsername.ParameterName = "@Username";
            newUsername.Value = username;
            cmd.Parameters.Add(newUsername);

            MySqlParameter newPassword = new MySqlParameter();
            newPassword.ParameterName = "@Password";
            newPassword.Value = password;
            cmd.Parameters.Add(newPassword);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            int count = 0;
            while(rdr.Read())
            {
                count = rdr.GetInt32(0);
            }

            DB.Close(conn);

            return count == 1;
        }

        public static ERUser Get(string username)
        {
            ERUser result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT id, username FROM users WHERE username = @Username;";

            MySqlParameter nameParam = new MySqlParameter();
            nameParam.ParameterName = "@Username";
            nameParam.Value = username;
            cmd.Parameters.Add(nameParam);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string newUsername = rdr.GetString(1);

                result = new ERUser(newUsername, newID);
            }

            DB.Close(conn);

            return result;
        }

        public static ERUser Get(int id)
        {
            ERUser result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT id, username FROM users WHERE id = {id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string username = rdr.GetString(1);

                result = new ERUser(username, newID);
            }

            DB.Close(conn);

            return result;
        }

        public static bool Exists(string username)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT COUNT(id) FROM users WHERE username = @Username;";

            MySqlParameter nameParam = new MySqlParameter();
            nameParam.ParameterName = "@Username";
            nameParam.Value = username;
            cmd.Parameters.Add(nameParam);

            int count = 0;

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                count = rdr.GetInt32(0);
            }

            DB.Close(conn);

            return count == 1;
        }

        public static int Count()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT COUNT(id) FROM users;";
            int count = 0;

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                count = rdr.GetInt32(0);
            }

            DB.Close(conn);

            return count;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"DELETE FROM users;";
            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }
    }
}
