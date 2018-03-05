using EpicReddit;
namespace EpicReddit.Tests
{
    public abstract class DBTest
    {
        public DBTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=epicreddit_test;";
        }
    }
}
