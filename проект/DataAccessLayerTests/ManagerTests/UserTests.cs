using NUnit.Framework;
using DataAccessLayer.Entities;
using System;
using DataAccessLayer.Repository;
using System.Linq;
using System.Collections.Generic;
using System.IO;
namespace DataAccessLayerTests.ManagerTests
{
    [TestFixture]
    class UserTests
    {
        #region constants
        static readonly ApplicationManager<User> userManager = new ApplicationManager<User>();
        static readonly UserProfile UserProfile = new UserProfile
        {
            BirthDate = DateTime.Now,
         //   Id=userManager.Table.First().Id,
            CurrentLocation = "Ternopil",
            IsOnline = true,
            LastTimeOnline = DateTime.Now,
            Name = "Ihor",
            Surname = "Hrubel"
        };
        User user = new User
        {
            UserName = "KonyokIhoryok",
            PasswordHash = "password"

        };
#endregion

        static List<User> toList(ApplicationManager<User> uManager)
        {
            try
            {
                return uManager.Table.Select(s => s).ToList();
            }
            catch (Exception)
            {

                return new List<User>();
            }
        }

        [Test]
        public void User_Insert()
        {
            var lenBefore = toList(userManager).Count;
            userManager.Insert(user);
            var lenAfter = toList(userManager).Count;
            Assert.AreEqual(lenBefore + 1, lenAfter);
        }

        [Test]
        public void User_Read()
        {
           var curUser= userManager.Table.First();
           var userToCheck= userManager.GetById(curUser.Id);
            File.WriteAllText(@"E:\ee.txt",userToCheck.UserName);
            Assert.AreEqual(curUser,userToCheck);
        }
        [Test]
        public void User_Update()
        {
            var qq=new ApplicationManager<UserProfile>();
            qq.Insert(UserProfile);
            var curUser = userManager.Table.First();
            var a = curUser.UserName;
            curUser.UserName = curUser.UserName + "1";
            userManager.Update(curUser);
            Assert.AreNotEqual(a,curUser.UserName);
        }
    }
}