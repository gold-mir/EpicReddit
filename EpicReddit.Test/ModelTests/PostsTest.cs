using System;
using EpicReddit;
using EpicReddit.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EpicReddit.Tests
{
    [TestClass]
    public class PostsTest : DBTest, IDisposable
    {
        public void Dispose()
        {
            Post.DeleteAll();
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Post.GetAll().Length;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAll_GetsAllPosts()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");

            newPost.Save();

            Assert.AreEqual(1, Post.GetAll().Length);
        }

        [TestMethod]
        public void Post_Save_ErrorsIfAlreadySaved()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();
            Exception ex = null;

            try
            {
                newPost.Save();
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void PostID_IsNegativeIfNotSaved()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            int unsavedID = newPost.GetID();
            int savedID;

            newPost.Save();
            savedID = newPost.GetID();

            Assert.AreEqual(-1, unsavedID);
            Assert.AreNotEqual(-1, savedID);
        }

        [TestMethod]
        public void GetByID_GetsCorrectPost()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();

            Post newPostFromDB = Post.GetByID(newPost.GetID());

            Assert.AreEqual(newPost.GetID(), newPostFromDB.GetID());
            Assert.AreEqual(newPost.GetTitle(), newPostFromDB.GetTitle());
        }

        [TestMethod]
        public void GetByID_ThrowsExceptionOnBadID()
        {
            Exception ex = null;

            try
            {
                Post post = Post.GetByID(42);
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Post_Delete_DeletesPost()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();
            int id = newPost.GetID();
            Exception ex = null;

            newPost.Delete();

            try
            {
                Post post = Post.GetByID(id);
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.AreEqual(-1, newPost.GetID());
        }

        [TestMethod]
        public void Post_Delete_ErrorIfNotSaved()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            Exception ex = null;

            try
            {
                newPost.Delete();
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Post_Edit_EditsBody()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();
            string oldBody = newPost.GetBody();
            string newBody = "I really like cats";

            newPost.Edit(newBody);

            Assert.AreEqual(newBody, newPost.GetBody());
            Assert.AreNotEqual(oldBody, newPost.GetBody());
        }

        [TestMethod]
        public void Post_Edit_ErrorIfNotSaved()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            Exception ex = null;

            try
            {
                newPost.Edit("blah");
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void GetPage_GetsItemsOnCorrectPage()
        {
            Post[] posts = new Post[9];
            for(int i = 0; i < posts.Length; i++)
            {
                string name = $"New Post #{i+1}";
                string body = $"This is the body of the post";
                string userName = $"user{i}";

                Post newPost = new Post(name, body, userName);
                newPost.Save();
                posts[i] = newPost;
            }

            Post[] page1 = Post.GetPage(1, 5);
            Post[] page2 = Post.GetPage(2, 5);

            Assert.AreEqual(5, page1.Length);
            Assert.AreEqual(4, page2.Length);

            for(int i = 0; i < page1.Length; i++)
            {
                Assert.AreEqual(posts[i].GetID(), page1[i].GetID());
            }

            for(int i = 0; i < page2.Length; i++)
            {
                int postIndex = i + 5;
                Assert.AreEqual(posts[postIndex].GetID(), page2[i].GetID());
            }
        }

        [TestMethod]
        public void Post_IsSaved_ChecksDB()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();
            Post newPostFromDB = Post.GetByID(newPost.GetID());
            newPost.Delete();

            Assert.IsFalse(newPostFromDB.IsSaved());
        }

        [TestMethod]
        public void Post_GetComments_0IfEmpty()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            newPost.Save();

            Comment[] comments = newPost.GetComments();

            Assert.AreEqual(0, comments.Length);
        }

        [TestMethod]
        public void Post_GetComments_GetsComments()
        {
            Post newPost = new Post("I like dogs", "I really like dogs", "dog_lover_78").Save();

            Comment newComment = new Comment("This is stupid", "dumb_idiot_12", newPost.GetID()).Save();

            Comment[] comments = newPost.GetComments();
            Assert.AreEqual(1, comments.Length);
            Assert.AreEqual(newComment.GetID(), comments[0].GetID());
        }
    }
}
