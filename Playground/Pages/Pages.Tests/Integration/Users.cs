using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Tests.Integration
{
    [TestClass]
    [TestCategory("Integration")]
    [TestCategory("Integration.Users")]
    [TestCategory("Users")]
    public class Users
    {
        private IUserRepository UserRepository;

        private IThemeRepository ThemeRepository;

        [TestInitialize]
        public void Initialize()
        {
            DbContextOptions<PagesDbContext> options = new DbContextOptionsBuilder<PagesDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
                .Options;
            PagesDbContext db = new PagesDbContext(options);

            this.UserRepository = new UserRepository(db);
            this.ThemeRepository = new ThemeRepository(db);
        }

        [TestMethod]
        public void InsertNewUserIntoDatabase()
        {
            User user = new User();

            UserRepository.Create(user);
            Assert.AreEqual(typeof(User), UserRepository.GetUser(user.Id) != null ? UserRepository.GetUser(user.Id).GetType() : null);
        }

        [TestMethod]
        public void InsertNewUserWithThemesIntoDatabase()
        {
            User user = new User();

            List<Theme> themes = new List<Theme>()
            {
                new Theme()
                {
                    Color = "ABC",
                    Name = "Test Theme 1"
                },
                new Theme()
                {
                    Color = "DEF",
                    Name = "Test Theme 2"
                },
                new Theme()
                {
                    Color = "987",
                    Name = "Test Theme 3"
                }
            };

            user.Themes = themes;

            UserRepository.Create(user);

            Assert.AreEqual(typeof(User), UserRepository.GetUser(user.Id) != null ? UserRepository.GetUser(user.Id).GetType() : null);
            Assert.AreEqual(themes.Count, UserRepository.GetUser(user.Id) != null ? UserRepository.GetUser(user.Id).Themes.Count : 0);
        }

        [TestMethod]
        public void UpdateAllUserInfoWithThemesInDatabase()
        {
            string
                NewName = "New Test Name",
                NewThemeName = "New Test Theme Name";

            User user = new User()
            {
                Email = "Test@Test.Test",
                Name = "Test Testens",
                Password = "Test123"
            };

            List<Theme> themes = new List<Theme>()
            {
                new Theme()
                {
                    Color = "ABC",
                    Name = "Test Theme 1"
                },
                new Theme()
                {
                    Color = "DEF",
                    Name = "Test Theme 2"
                },
                new Theme()
                {
                    Color = "987",
                    Name = "Test Theme 3"
                }
            };

            user.Themes = themes;

            UserRepository.Create(user);

            if(UserRepository.GetUser(user.Id) == null)
            {
                Assert.Fail($"A user with user.Id {user.Id} was not found.");
            }

            Assert.AreEqual(typeof(User), UserRepository.GetUser(user.Id).GetType());
            Assert.AreEqual(themes.Count, UserRepository.GetUser(user.Id).Themes.Count);

            user.Name = NewName;
            user.Themes.FirstOrDefault().Name = NewThemeName;
            UserRepository.Update(user);

            Assert.AreEqual(NewName, UserRepository.GetUser(user.Id).Name);
            Assert.AreEqual(1, UserRepository.GetUser(user.Id).Themes.Where(t => t.Name.Equals(NewThemeName)).Count());
            Assert.AreEqual(themes.Count, UserRepository.GetUser(user.Id).Themes.Count);
        }

        [TestMethod]
        public void DeleteUserWithThemesInDatabase()
        {
            User user = new User();

            List<Theme> themes = new List<Theme>()
            {
                new Theme()
                {
                    Color = "ABC",
                    Name = "Test Theme 1"
                },
                new Theme()
                {
                    Color = "DEF",
                    Name = "Test Theme 2"
                },
                new Theme()
                {
                    Color = "987",
                    Name = "Test Theme 3"
                }
            };

            user.Themes = themes;

            UserRepository.Create(user);

            if(UserRepository.GetUser(user.Id) == null)
            {
                Assert.Fail($"A user with user.Id {user.Id} was not found.");
            }

            Assert.AreEqual(typeof(User), UserRepository.GetUser(user.Id).GetType());
            Assert.AreEqual(themes.Count, UserRepository.GetUser(user.Id).Themes.Count);

            UserRepository.Delete(user);

            Assert.AreEqual(0, UserRepository.GetUsers().Where(u => u.Id.Equals(user.Id)).Count());
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }
    }
}