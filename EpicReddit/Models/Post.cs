namespace EpicReddit.Models
{
    public class Post {
        public string Title;
        public string Body;
        public int id;
        public string posterName;

        public Comment[] GetComments()
        {
            throw new NotImplementedException();
        }
    }
}
