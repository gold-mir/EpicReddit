using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EpicReddit.Models
{
    public class Post {
        private string _title;
        private string _body;
        private int _id;
        private string _posterName;

        public Post(string title, string body, string posterName)
        {
            _title = title;
            _body = body;
            _posterName = posterName;
            _id = -1;
        }

        public int GetID()
        {
            return _id;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetBody()
        {
            return _body;
        }

        public string GetPosterName()
        {
            return _posterName;
        }

        public bool IsSaved()
        {
            return _id != -1;
        }

        public void Save()
        {
            if(IsSaved())
            {
                throw new Exception("Cannot save an already saved post.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "INSERT INTO posts (title, body, username) VALUES (@Title, @Body, @Username);";

            MySqlParameter newTitle = new MySqlParameter();
            newTitle.ParameterName = "@Title";
            newTitle.Value = _title;

            MySqlParameter newBody = new MySqlParameter();
            newBody.ParameterName = "@Body";
            newBody.Value = _body;

            MySqlParameter newUsername = new MySqlParameter();
            newUsername.ParameterName = "@Username";
            newUsername.Value = _posterName;

            cmd.Parameters.Add(newTitle);
            cmd.Parameters.Add(newBody);
            cmd.Parameters.Add(newUsername);

            cmd.ExecuteNonQuery();

            _id = (int)cmd.LastInsertedId;

            DB.Close(conn);
        }

        public Comment[] GetComments()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            if(!IsSaved())
            {
                throw new Exception("Can't delete a post that hasn't been saved.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"DELETE FROM posts WHERE id = {_id};";

            cmd.ExecuteNonQuery();

            DB.Close(conn);
            _id = -1;
        }

        public void Edit(string newBody)
        {
            if(!IsSaved())
            {
                throw new Exception("Can't edit a post that hasn't been saved.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"UPDATE posts SET body = @Body WHERE id = {_id};";

            MySqlParameter updateBody = new MySqlParameter();
            updateBody.ParameterName = "@Body";
            updateBody.Value = newBody;

            cmd.Parameters.Add(updateBody);

            cmd.ExecuteNonQuery();

            DB.Close(conn);
            _body = newBody;
        }

        public static Post GetByID(int id)
        {
            Post result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM posts WHERE id = {id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                string body = rdr.GetString(2);
                string username = rdr.GetString(3);

                result = new Post(title, body, username);
                result._id = newID;
            }

            DB.Close(conn);

            if(result == null)
            {
                throw new Exception($"Couldn't find post with id {id}");
            }

            return result;
        }

        public static Post[] GetAll()
        {
            List<Post> result = new List<Post>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT * FROM posts ORDER BY id ASC;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                string body = rdr.GetString(2);
                string username = rdr.GetString(3);

                Post newPost = new Post(title, body, username);
                newPost._id = id;
                result.Add(newPost);
            }

            DB.Close(conn);

            return result.ToArray();
        }

        public static Post[] GetPage(int pageNumber, int itemsPerPage)
        {
            if(pageNumber < 0 || itemsPerPage < 1)
            {
                throw new Exception("Can't get negative page ID or zero items per page");
            }

            List<Post> result = new List<Post>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM posts ORDER BY id ASC LIMIT {(pageNumber - 1) * itemsPerPage},{itemsPerPage};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                string body = rdr.GetString(2);
                string username = rdr.GetString(3);

                Post newPost = new Post(title, body, username);
                newPost._id = id;
                result.Add(newPost);
            }

            DB.Close(conn);

            return result.ToArray();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "DELETE FROM posts;";
            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }
    }
}
