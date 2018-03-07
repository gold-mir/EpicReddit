using System;
using EpicReddit;
using EpicReddit.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EpicReddit.Tests
{
    [TestClass]
    public class UsersTest : DBTest, IDisposable
    {
        public void Dispose()
        {
            User.DeleteAll();
        }

        [TestMethod]
        public void UserCount_StartsAt0()
        {
            int count = User.Count();

            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CreateUser_CreatesUser()
        {
            User me = User.Create("gold-mir", "insecurepassword43");

            Assert.AreEqual(1, User.Count());
        }

        [TestMethod]
        public void CreateUser_ErrorsOnDuplicateEntry()
        {
            User me = User.Create("gold-mir", "insecurepassword43");
            Exception ex = null;

            try
            {
                User doppelganger = User.Create("gold-mir", "insecurepassword43");
            } catch (Exception e)
            {
                Assert.IsNotInstanceOfType(e, typeof(NotImplementedException));
                ex = e;
            }

            Assert.IsNotNull(ex);
        }

        [TestMethod]
        public void UserExists_ChecksForExistingUser()
        {
            User me = User.Create("gold-mir", "insecurepassword43");

            Assert.IsTrue(User.Exists("gold-mir"));
            Assert.IsFalse(User.Exists("iron-myr"));
        }

        [TestMethod]
        public void GetUserByID_GetsCorrectUser()
        {
            User me = User.Create("gold-mir", "insecurepassword43");
            int myid = me.GetID();

            User meByID = User.Get(myid);

            Assert.AreEqual(me.GetID(), meByID.GetID());
        }

        [TestMethod]
        public void GetUserByName_GetsCorrectUser()
        {
            User me = User.Create("gold-mir", "insecurepassword43");
            string myName = me.GetUsername();

            User meByName = User.Get(myName);

            Assert.AreEqual(me.GetID(), meByName.GetID());
        }

        [TestMethod]
        public void User_ValidatePassword_ValidatesPassword()
        {
            string myPassword = "thisismypassword";
            string notMyPassword = "thisisnotmypassword";
            User me = User.Create("gold-mir", myPassword);

            Assert.IsTrue(me.ValidatePassword(myPassword));
            Assert.IsFalse(me.ValidatePassword(notMyPassword));
        }

        [TestMethod]
        public void User_ValidateUserInfo_WorksCorrectly()
        {
            string myName = "gold-mir";
            string notMyName = "iron-mir";
            string myPassword = "thisismypassword";
            string notMyPassword = "thisisnotmypassword";
            User me = User.Create(myName, myPassword);

            Assert.IsTrue(User.ValidateUserInfo(myName, myPassword));
            Assert.IsFalse(User.ValidateUserInfo(myName, notMyPassword));
            Assert.IsFalse(User.ValidateUserInfo(notMyName, myPassword));
        }
    }
}
