using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EpicReddit.Models
{
    public class Post
    {
        private string _title;
        private string _body;
        private int _id;
        private int _userID;

        public Post(string title, string body, int userID, int id = -1)
        {
            _title = title;
            _body = body;
            _userID = userID;
            _id = id;
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

        public int GetUserID()
        {
            return _userID;
        }

        public bool IsSaved()
        {
            if(_id == -1)
            {
                return false;
            } else {
                int count = 0;
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"SELECT COUNT(id) FROM posts WHERE id = {_id};";

                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                while(rdr.Read())
                {
                    count = rdr.GetInt32(0);
                }

                DB.Close(conn);

                return count != 0;
            }
        }

        private void AssertIsSaved()
        {
            if(!IsSaved())
            {
                throw new Exception("Cannot perform this operation on a post that hasn't been saved to the database.");
            }
        }

        public Post Save()
        {
            if(IsSaved())
            {
                throw new Exception("Cannot save an already saved post.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "INSERT INTO posts (title, body, user_id) VALUES (@Title, @Body, @UserID);";

            MySqlParameter newTitle = new MySqlParameter();
            newTitle.ParameterName = "@Title";
            newTitle.Value = _title;

            MySqlParameter newBody = new MySqlParameter();
            newBody.ParameterName = "@Body";
            newBody.Value = _body;

            MySqlParameter newUserID = new MySqlParameter();
            newUserID.ParameterName = "@UserID";
            newUserID.Value = _userID;

            cmd.Parameters.Add(newTitle);
            cmd.Parameters.Add(newBody);
            cmd.Parameters.Add(newUserID);

            cmd.ExecuteNonQuery();

            _id = (int)cmd.LastInsertedId;

            DB.Close(conn);
            return this;
        }

        public void Delete()
        {
            AssertIsSaved();

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
            AssertIsSaved();

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

        public Comment[] GetComments()
        {
            AssertIsSaved();

            List<Comment> result = new List<Comment>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM comments WHERE comments.post_id = {_id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string body = rdr.GetString(1);
                int userID = rdr.GetInt32(2);
                int postID = rdr.GetInt32(3);
                int parentCommentID;
                if(!rdr.IsDBNull(4))
                {
                    parentCommentID = rdr.GetInt32(4);
                } else {
                    parentCommentID = -1;
                }

                Comment newComment = new Comment(body, userID, postID, newID, parentCommentID);
                result.Add(newComment);
            }

            DB.Close(conn);
            return result.ToArray();
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
                int userID = rdr.GetInt32(3);

                result = new Post(title, body, userID);
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
                int userID = rdr.GetInt32(3);

                Post newPost = new Post(title, body, userID);
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
                int userID = rdr.GetInt32(3);

                Post newPost = new Post(title, body, userID);
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
