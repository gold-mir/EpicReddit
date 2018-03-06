using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EpicReddit.Models
{
    public class Comment
    {
        private string _username;
        private string _body;
        private int _postID;
        private int _parentCommentID;
        private int _id;

        public Comment(string body, string username, int postID)
        {
            _body = body;
            _username = username;
            _postID = postID;
            _id = -1;
            _parentCommentID = -1;
        }

        public bool IsSaved()
        {
            if(_id == -1)
            {
                return false;
            } else {
                bool result = false;
                MySqlConnection conn = DB.Connection();
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
                cmd.CommandText = $"SELECT username FROM comments WHERE id = {_id};";

                MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                while(rdr.Read())
                {
                    string username = rdr.GetString(0);
                    if(username != "")
                    {
                        result = true;
                    }
                }
                DB.Close(conn);

                return result;
            }
        }

        public int GetID()
        {
            return _id;
        }

        public string GetUsername()
        {
            return _username;
        }

        public string GetBody()
        {
            return _body;
        }

        public int GetPostID()
        {
            return _postID;
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
            cmd.CommandText = "INSERT INTO comments (body, username, post_id) VALUES (@Body, @Username, @postID);";

            MySqlParameter newBody = new MySqlParameter();
            newBody.ParameterName = "@Body";
            newBody.Value = _body;

            MySqlParameter newUsername = new MySqlParameter();
            newUsername.ParameterName = "@Username";
            newUsername.Value = _username;

            MySqlParameter newPostID = new MySqlParameter();
            newPostID.ParameterName = "@postID";
            newPostID.Value = _postID;


            cmd.Parameters.Add(newBody);
            cmd.Parameters.Add(newUsername);
            cmd.Parameters.Add(newPostID);

            cmd.ExecuteNonQuery();

            _id = (int)cmd.LastInsertedId;

            DB.Close(conn);
        }

        public void Delete()
        {
            if(!IsSaved())
            {
                throw new Exception("Can't delete a post that hasn't been saved to the database.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"DELETE FROM comments WHERE id = {_id};";

            cmd.ExecuteNonQuery();

            DB.Close(conn);
            _id = -1;
        }

        public void Edit(string newBody)
        {
            if(!IsSaved())
            {
                throw new Exception("Can't edit a post taht hasn't been saved to the database.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"UPDATE comments SET body = @Body WHERE id = {_id};";

            MySqlParameter updateBody = new MySqlParameter();
            updateBody.ParameterName = "@Body";
            updateBody.Value = newBody;
            cmd.Parameters.Add(updateBody);

            cmd.ExecuteNonQuery();

            DB.Close(conn);
            _body = newBody;
        }

        public Post GetParentPost()
        {
            throw new NotImplementedException();
        }

        public Comment GetParentComment()
        {
            throw new NotImplementedException();
        }

        public Comment[] GetChildren()
        {
            throw new NotImplementedException();
        }

        public void AddChild(Comment comment)
        {
            throw new NotImplementedException();
        }

        public static Comment[] GetAll()
        {
            List<Comment> result = new List<Comment>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT * FROM comments;";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string body = rdr.GetString(1);
                string username = rdr.GetString(2);
                int postID = rdr.GetInt32(3);
                int parentCommentID;
                if(!rdr.IsDBNull(4))
                {
                    parentCommentID = rdr.GetInt32(4);
                } else {
                    parentCommentID = -1;
                }

                Comment newComment = new Comment(body, username, postID);
                newComment._id = id;
                newComment._parentCommentID = parentCommentID;
                result.Add(newComment);
            }

            DB.Close(conn);

            return result.ToArray();
        }

        public static Comment GetByID(int id)
        {
            Comment result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM comments WHERE id = {id};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

            while(rdr.Read())
            {
                int newID = rdr.GetInt32(0);
                string body = rdr.GetString(1);
                string username = rdr.GetString(2);
                int postID = rdr.GetInt32(3);
                int parentCommentID;
                if(!rdr.IsDBNull(4))
                {
                    parentCommentID = rdr.GetInt32(4);
                } else {
                    parentCommentID = -1;
                }

                Comment newComment = new Comment(body, username, postID);
                newComment._id = newID;
                newComment._parentCommentID = parentCommentID;
                result = newComment;
            }

            DB.Close(conn);

            if(result == null)
            {
                throw new Exception($"Couldn't find comment with id {id}");
            }

            return result;
        }

        public static Comment[] GetChildrenOfPost(Post post)
        {
            throw new NotImplementedException();
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "DELETE FROM comments;";

            cmd.ExecuteNonQuery();

            DB.Close(conn);
        }
    }
}
