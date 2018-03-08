using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace EpicReddit.Models
{
    public class Comment
    {
        private int _userID;
        private string _body;
        private int _postID;
        private int _parentCommentID;
        private int _id;

        public Comment(string body, int userID, int postID, int id = -1, int parentCommentID = -1)
        {
            _body = body;
            _userID = userID;
            _postID = postID;
            _id = id;
            _parentCommentID = parentCommentID;
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
                cmd.CommandText = $"SELECT COUNT(id) FROM comments WHERE id = {_id};";

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
                throw new Exception("Can't perform this operation on a comment that hasn't been saved to the database.");
            }
        }

        public int GetID()
        {
            return _id;
        }

        public int GetUserID()
        {
            return _userID;
        }

        public string GetBody()
        {
            return _body;
        }

        public int GetPostID()
        {
            return _postID;
        }

        public Comment Save()
        {
            if(IsSaved())
            {
                throw new Exception("Cannot save an already saved post.");
            }

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "INSERT INTO comments (body, user_id, post_id) VALUES (@Body, @UserID, @postID);";

            MySqlParameter newBody = new MySqlParameter();
            newBody.ParameterName = "@Body";
            newBody.Value = _body;

            MySqlParameter newUserID = new MySqlParameter();
            newUserID.ParameterName = "@UserID";
            newUserID.Value = _userID;

            MySqlParameter newPostID = new MySqlParameter();
            newPostID.ParameterName = "@postID";
            newPostID.Value = _postID;


            cmd.Parameters.Add(newBody);
            cmd.Parameters.Add(newUserID);
            cmd.Parameters.Add(newPostID);

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
            cmd.CommandText = $"DELETE FROM comments WHERE id = {_id};";

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
            cmd.CommandText = $"UPDATE comments SET body = @Body WHERE id = {_id};";

            MySqlParameter updateBody = new MySqlParameter();
            updateBody.ParameterName = "@Body";
            updateBody.Value = newBody;
            cmd.Parameters.Add(updateBody);

            cmd.ExecuteNonQuery();

            DB.Close(conn);
            _body = newBody;
        }

        public ERUser GetUser()
        {
            return ERUser.Get(_userID);
        }

        public Post GetParentPost()
        {
            AssertIsSaved();

            Post parent = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM posts WHERE id = {_postID};";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int parentID = rdr.GetInt32(0);
                string parentTitle = rdr.GetString(1);
                string parentBody = rdr.GetString(2);
                int userID = rdr.GetInt32(3);

                parent = new Post(parentTitle, parentBody, userID, parentID);
            }

            DB.Close(conn);

            if(parent == null)
            {
                throw new Exception("Something is very wrong you should never see this.");
            }

            return parent;
        }

        public Comment GetParentComment()
        {
            AssertIsSaved();
            if(_parentCommentID == -1)
            {
                return null;
            }

            Comment result = null;

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM comments WHERE id = {_parentCommentID};";

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

                result = new Comment(body, userID, postID, newID);
                result._parentCommentID = parentCommentID;
            }

            DB.Close(conn);

            return result;
        }

        public Comment[] GetChildren()
        {
            AssertIsSaved();

            List<Comment> children = new List<Comment>();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"SELECT * FROM comments WHERE post_id = {_postID};";
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

                Comment newComment = new Comment(body, userID, postID, newID);
                newComment._parentCommentID = parentCommentID;
                children.Add(newComment);
            }

            DB.Close(conn);

            return GetAllChildren(children, _id);
        }

        private static Comment[] GetAllChildren(List<Comment> comments, int currentID)
        {
            List<Comment> result = new List<Comment>();

            foreach(Comment comment in comments)
            {
                if(comment._parentCommentID == currentID)
                {
                    result.Add(comment);
                    result.AddRange(GetAllChildren(comments, comment._id));
                }
            }

            return result.ToArray();
        }

        public void AddChild(Comment child)
        {
            AssertIsSaved();
            child.AssertIsSaved();

            MySqlConnection conn = DB.Connection();
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = $"UPDATE comments SET parent_id = {_id} WHERE id = {child.GetID()};";

            cmd.ExecuteNonQuery();

            DB.Close(conn);

            child._parentCommentID = _id;
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
                int userID = rdr.GetInt32(2);
                int postID = rdr.GetInt32(3);
                int parentCommentID;
                if(!rdr.IsDBNull(4))
                {
                    parentCommentID = rdr.GetInt32(4);
                } else {
                    parentCommentID = -1;
                }

                Comment newComment = new Comment(body, userID, postID);
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
                int userID = rdr.GetInt32(2);
                int postID = rdr.GetInt32(3);
                int parentCommentID;
                if(!rdr.IsDBNull(4))
                {
                    parentCommentID = rdr.GetInt32(4);
                } else {
                    parentCommentID = -1;
                }

                Comment newComment = new Comment(body, userID, postID);
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
