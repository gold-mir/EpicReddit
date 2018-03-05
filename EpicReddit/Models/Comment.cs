namespace EpicReddit.Models
{
    public class Comment
    {
        public string UserName;
        public string Body;
        public int postID;
        public int parentCommentID;
        public int id;

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
    }
}
