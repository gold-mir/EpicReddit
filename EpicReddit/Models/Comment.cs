using System;

namespace EpicReddit.Models
{
    public class Comment
    {
        private string _username;
        private string _body;
        private int _postID;
        private int _parentCommentID;
        private int _id;

        public Comment(string body, string username, int postID, int parentCommentID = -1)
        {
            _body = body;
            _username = username;
            _postID = postID;
            _parentCommentID = parentCommentID;
            _id = -1;
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

        public int GetParentCommentID()
        {
            return _parentCommentID;
        }

        public void Edit(string newBody)
        {
            throw new NotImplementedException();
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

        public static Comment[] GetAll()
        {
            throw new NotImplementedException();
        }

        public static Comment GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public static void DeleteAll()
        {
            throw new NotImplementedException();
        }
    }
}
