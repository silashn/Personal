using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Models;
using System;
using Pages.Data.Repositories.Membership;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Tests.UnitTests
{
    [TestClass]
    public class UserTests
    {
        public IUserRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<PagesDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            this.repository = new UserRepository(new PagesDbContext(options));
        }

        [TestMethod]
        public void UserNameDoesNotAllowOver64Characters()
        {
            try
            {
                Users user = new Users()
                {
                    Name = new string('x', 100)
                };
                Assert.Fail("User name allowed over 64 characters.");
            }
            catch(Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }

        }

        [TestMethod]
        public void InsertNewUserIntoDatabase()
        {
            Users user = new Users();

            repository.Create(user);
            Assert.AreEqual(typeof(Users), repository.GetUser(user.Id) != null ? repository.GetUser(user.Id).GetType() : null);
        }

        [TestMethod]
        public void InsertNewUserWithThemesIntoDatabase()
        {
            Users user = new Users();

            List<Themes> themes = new List<Themes>()
            {
                new Themes()
                {
                    Color = "ABC",
                    Name = "Test Theme 1"
                },
                new Themes()
                {
                    Color = "DEF",
                    Name = "Test Theme 2"
                },
                new Themes()
                {
                    Color = "987",
                    Name = "Test Theme 3"
                }
            };

            user.Themes = themes;

            repository.Create(user);

            Assert.AreEqual(typeof(Users), repository.GetUser(user.Id) != null ? repository.GetUser(user.Id).GetType() : null);
            Assert.AreEqual(themes.Count, repository.GetUser(user.Id) != null ? repository.GetUser(user.Id).Themes.Count : 0);
        }

        [TestMethod]
        public void UpdateAllUserInfoWithThemesInDatabase()
        {
            string 
                NewName = "New Test Name",
                NewThemeName = "New Test Theme Name";

            Users user = new Users()
            {
                Email = "Test@Test.Test",
                Name = "Test Testens",
                Password = "Test123"
            };

            List<Themes> themes = new List<Themes>()
            {
                new Themes()
                {
                    Color = "ABC",
                    Name = "Test Theme 1"
                },
                new Themes()
                {
                    Color = "DEF",
                    Name = "Test Theme 2"
                },
                new Themes()
                {
                    Color = "987",
                    Name = "Test Theme 3"
                }
            };

            user.Themes = themes;

            repository.Create(user);

            if(repository.GetUser(user.Id) == null)
            {
                Assert.Fail($"A user with user.Id {user.Id} was not found.");
            }

            Assert.AreEqual(typeof(Users), repository.GetUser(user.Id).GetType() );
            Assert.AreEqual(themes.Count, repository.GetUser(user.Id).Themes.Count);

            user.Name = NewName;
            user.Themes.FirstOrDefault().Name = NewThemeName;
            repository.Update(user);

            Assert.AreEqual(NewName, repository.GetUser(user.Id).Name);
            Assert.AreEqual(NewThemeName, repository.GetUser(user.Id).Themes.FirstOrDefault().Name);
            Assert.AreEqual(themes.Count, repository.GetUser(user.Id).Themes.Count);
        }
    }
}