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
    [TestCategory("Integration.Themes")]
    [TestCategory("Themes")]
    public class Themes
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
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertNewThemeIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            Theme theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme",
                UserId = user.Id
            };

            ThemeRepository.Create(theme);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf50ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.InsertVerbose")]
        public void InsertRangeOf50ThemesIntoDatabaseVerbose()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf100ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 100, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(100, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf1000ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.InsertVerbose")]
        public void InsertRangeOf1000ThemesIntoDatabaseVerbose()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteRangeOf50ThemesFromDatabase()
        {
            bool HasThemes = false;
            if(ThemeRepository.GetThemes().Count() > 0)
            {
                HasThemes = true;
            }

            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
            Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }


        [TestMethod]
        [TestCategory("Integraiton.Themes.DeleteVerbose")]
        public void DeleteRangeOf50ThemesFromDatabaseVerbose()
        {
            bool HasThemes = false;
            if(ThemeRepository.GetThemes().Count() > 0)
            {
                HasThemes = true;
            }

            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRangeVerbose(themes);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteRangeOf100ThemesFromDatabase()
        {
            bool HasThemes = false;
            if(ThemeRepository.GetThemes().Count() > 0)
            {
                HasThemes = true;
            }

            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 100, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(100, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteRangeOf1000ThemesFromDatabase()
        {
            bool HasThemes = false;
            if(ThemeRepository.GetThemes().Count() > 0)
            {
                HasThemes = true;
            }

            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRange(themes);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.DeleteVerbose")]
        public void DeleteRangeOf1000ThemesFromDatabaseVerbose()
        {
            bool HasThemes = false;
            if(ThemeRepository.GetThemes().Count() > 0)
            {
                HasThemes = true;
            }

            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRangeVerbose(themes);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteAllRemainingThemesFromDatabaseIfAnyExistsOtherwiseCreate10ThemesAndThenDeleteThemAllFromDatabase()
        {
            if(ThemeRepository.GetThemes().Count() < 1)
            {
                User user = new User()
                {
                    Name = "Test User",
                    Email = "Test@Email.dk",
                    Password = "test123"
                };

                UserRepository.Create(user);
                user = UserRepository.GetUser(user.Id);

                if(user == null)
                    Assert.Fail("User does not exist");

                List<Theme> themes = ReturnSeededListOfThemes(count: 10, userId: user.Id);
                ThemeRepository.CreateRange(themes);
            }

            ThemeRepository.DeleteRange(ThemeRepository.GetThemes().ToList());

            Assert.AreEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.DeleteVerbose")]
        public void DeleteAllRemainingThemesFromDatabaseIfAnyExistsOtherwiseCreate10ThemesAndThenDeleteThemAllFromDatabaseVerbose()
        {
            if(ThemeRepository.GetThemes().Count() < 1)
            {
                User user = new User()
                {
                    Name = "Test User",
                    Email = "Test@Email.dk",
                    Password = "test123"
                };

                UserRepository.Create(user);
                user = UserRepository.GetUser(user.Id);

                if(user == null)
                    Assert.Fail("User does not exist");

                List<Theme> themes = ReturnSeededListOfThemes(count: 10, userId: user.Id);
                ThemeRepository.CreateRangeVerbose(themes);
            }

            ThemeRepository.DeleteRangeVerbose(ThemeRepository.GetThemes().ToList());

            Assert.AreEqual(0, ThemeRepository.GetThemes().Count());
        }

        private List<Theme> ReturnSeededListOfThemes(int count, int? userId = null)
        {
            List<Theme> themes = new List<Theme>();

            for(int i = 0; i < count; i++)
            {

                Theme newTheme = new Theme()
                {
                    Color = i.ToString("X6"),
                    Name = "Theme from Range - " + i,
                    UserId = userId ?? 0
                };

                themes.Add(newTheme);
            }

            return themes;
        }
    }
}
