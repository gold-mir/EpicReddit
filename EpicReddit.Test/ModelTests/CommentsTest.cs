using System;
using EpicReddit;
using EpicReddit.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EpicReddit.Tests
{
    [TestClass]
    public class CommentsTest : DBTest, IDisposable
    {
        public void Dispose()
        {
            Post.DeleteAll();

            Comment.DeleteAll();
        }

        public Post defaultPost;

        public CommentsTest()
        {
            defaultPost = new Post("I like dogs", "I really like dogs", "dog_lover_78");
            defaultPost.Save();
        }

        [TestMethod]
        public void Comments_GetAll_StartsEmpty()
        {
            int count = Comment.GetAll().Length;
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void Comments_GetAll_GetsAllComments()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();

            Assert.AreEqual(1, Comment.GetAll().Length);
        }

        [TestMethod]
        public void Comment_Save_ErrorsIfAlreadySaved()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();

            Exception ex = null;

            try
            {
                newComment.Save();
            } catch (Exception e)
            {
                if(e.GetType() != typeof(NotImplementedException))
                {
                    ex = e;
                }
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Comment_Save_ErrorsIfBadPostID()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", -30);
            Exception ex = null;

            try
            {
                newComment.Save();
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Comment_GetID_IsNegativeIfNotSaved()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            int oldID = newComment.GetID();

            newComment.Save();

            Assert.AreNotEqual(-1, newComment.GetID());
            Assert.AreNotEqual(oldID, newComment.GetID());
        }

        [TestMethod]
        public void Comment_GetByID_GetsCommentByID()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();

            Comment newCommentFromDB = Comment.GetByID(newComment.GetID());

            Assert.AreEqual(newComment.GetID(), newCommentFromDB.GetID());
            Assert.AreEqual(newComment.GetBody(), newCommentFromDB.GetBody());
        }

        [TestMethod]
        public void Comment_GetByID_ErrorsOnBadID()
        {
            Exception ex = null;

            try
            {
                Comment comment = Comment.GetByID(42);
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Comment_Delete_DeletesComment()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();
            int id = newComment.GetID();
            Exception ex = null;

            newComment.Delete();
            try
            {
                Comment comment = Comment.GetByID(id);
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
            Assert.AreEqual(-1, newComment.GetID());
        }

        [TestMethod]
        public void Comment_Delete_ErrorsIfNotSaved()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Exception ex = null;

            try
            {
                newComment.Delete();
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Comment_IsSaved_ChecksFromDB()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();

            Comment newCommentFromDB = Comment.GetByID(newComment.GetID());
            newComment.Delete();

            Assert.IsFalse(newCommentFromDB.IsSaved());
        }

        [TestMethod]
        public void Comment_Edit_EditsBody()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();
            string oldBody = newComment.GetBody();
            string newBody = "I REALLY like cats";

            newComment.Edit(newBody);

            Assert.AreEqual(newBody, newComment.GetBody());
        }

        [TestMethod]
        public void Comment_Edit_ErrorsIfNotSaved()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Exception ex = null;

            try
            {
                newComment.Edit("blah");
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void Comment_GetParentPost_GetsCorrectPost()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();
            Post parent = newComment.GetParentPost();

            Assert.AreEqual(parent.GetID(), defaultPost.GetID());
            Assert.AreEqual(parent.GetTitle(), defaultPost.GetTitle());
        }

        [TestMethod]
        public void Comment_GetChildComments_DefaultIsEmpty()
        {
            Comment newComment = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            newComment.Save();

            Assert.AreEqual(0, newComment.GetChildren().Length);
        }

        [TestMethod]
        public void Comment_AddChild_AddsChildComment()
        {
            Comment parent = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Comment child = new Comment("I like dogs", "dog_lover_78", defaultPost.GetID());
            parent.Save();
            child.Save();

            parent.AddChild(child);

            Assert.AreEqual(1, parent.GetChildren().Length);
        }

        [TestMethod]
        public void Comment_AddChild_SetsCorrectParent()
        {
            Comment parent = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Comment child = new Comment("I like dogs", "dog_lover_78", defaultPost.GetID());
            parent.Save();
            child.Save();

            parent.AddChild(child);
            Comment parentAccordingToChild = child.GetParentComment();

            Assert.AreEqual(parentAccordingToChild.GetID(), parent.GetID());
        }

        [TestMethod]
        public void Comment_GetChildren_ReturnsCorrectChildren()
        {
            Comment parent = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Comment child = new Comment("I like dogs", "dog_lover_78", defaultPost.GetID());
            parent.Save();
            child.Save();

            parent.AddChild(child);
            Comment[] children = parent.GetChildren();
            Comment childAccordingToParent = children[0];

            Assert.AreEqual(child.GetID(), childAccordingToParent.GetID());
        }

        [TestMethod]
        public void Comment_Delete_DeletesChildren()
        {
            Comment parent = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Comment child = new Comment("I like dogs", "dog_lover_78", defaultPost.GetID());
            parent.Save();
            child.Save();

            parent.AddChild(child);
            parent.Delete();

            Assert.IsFalse(child.IsSaved());
        }

        [TestMethod]
        public void Comment_Delete_DeletesAllChildren()
        {
            Comment parent = new Comment("I like cats", "cat_lover", defaultPost.GetID());
            Comment child = new Comment("I like dogs", "dog_lover_78", defaultPost.GetID());
            Comment deepChild = new Comment("I like frogs", "frog_luvr", defaultPost.GetID());
            parent.Save();
            child.Save();
            deepChild.Save();

            parent.AddChild(child);
            child.AddChild(deepChild);

            parent.Delete();

            Assert.IsFalse(deepChild.IsSaved());
        }
    }
}
