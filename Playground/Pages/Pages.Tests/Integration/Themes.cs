using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using Pages.Tests.Tools;
using System;
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

        public TestContext TestContext { get; set; }

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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            Theme theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme",
                UserId = user.Id
            };

            ThemeRepository.Create(theme).FormatHtml().Output(TestContext);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertNewThemeWithTakenNameIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            Theme theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme",
                UserId = user.Id
            };

            ThemeRepository.Create(theme).FormatHtml().Output(TestContext);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id) && t.Name.Equals(theme.Name)).Count());

            theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme 2",
                UserId = user.Id
            };

            ThemeRepository.Create(theme).FormatHtml().Output(TestContext);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id) && t.Name.Equals(theme.Name)).Count());

            theme = new Theme()
            {
                Color = "BBB",
                Name = "Test Theme 2",
                UserId = user.Id
            };

            try
            {
                string s = ThemeRepository.Create(theme);
                s.FormatHtml().Output(TestContext);
                if(s.ToLower().Contains("error"))
                    throw new InvalidOperationException("Caught in DB");
                Assert.Fail("Allowed same theme name twice on one user");
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(AssertFailedException))
                    Assert.Fail(ex.Message);
                else
                    ex.Message.FormatHtml().Output(TestContext);
                Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }

            user = new User()
            {
                Name = "Test User 2",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme",
                UserId = user.Id
            };

            ThemeRepository.Create(theme).FormatHtml().Output(TestContext);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id) && t.Name.Equals(theme.Name)).Count());
        }

        [TestMethod]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertNewThemeListWithTakenNameIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            Theme theme = new Theme()
            {
                Color = "AAA",
                Name = "Test Theme",
                UserId = user.Id
            };

            ThemeRepository.Create(theme).FormatHtml().Output(TestContext);

            Assert.AreEqual(1, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            List<Theme> themes = new List<Theme>()
            {
                new Theme()
                {
                    Color = "AAA",
                    Name = "Test Theme 2",
                    UserId = user.Id
                },
                new Theme()
                {
                    Color = "BBB",
                    Name = "Test Theme 2",
                    UserId = user.Id
                }
            };

            try
            {
                if(themes.Any(t => themes.Any(_t => _t != t && _t.Name.Equals(t.Name) && _t.UserId.Equals(t.UserId))))
                {
                    throw new InvalidOperationException("Caught in list.");
                }

                string s = ThemeRepository.CreateRangeVerbose(themes);
                s.FormatHtml().Output(TestContext);
                if(s.ToLower().Contains("error"))
                    throw new InvalidOperationException("Caught in DB");
                Assert.Fail("Allowed same theme name twice on one user");
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(AssertFailedException))
                    Assert.Fail(ex.Message);
                else
                    ex.Message.FormatHtml().Output(TestContext);
                    Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRange(themes).Output(TestContext);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes).Output(TestContext);
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(50, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRangeVerbose(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        #region Benchmark
        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf100ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 100, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(100, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf1000ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.Insert")]
        public void InsertRangeOf10000ThemesIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 10000, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(10000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.InsertVerbose")]
        public void InsertRangeOf1000ThemesIntoDatabaseVerbose()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.InsertVerbose")]
        public void InsertRangeOf10000ThemesIntoDatabaseVerbose()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 10000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(10000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.InsertVerbose")]
        public void InsertRangeOf50000ThemesIntoDatabaseVerbose()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 50000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(50000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 100, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(100, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteRangeOf100000ThemesFromDatabase()
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 100000, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(100000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.Delete")]
        public void DeleteRangeOf1000000ThemesFromDatabase()
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000000, userId: user.Id);
            ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(1000000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRange(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());
            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 1000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(1000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRangeVerbose(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }

        [TestMethod]
        [TestCategory("Benchmark")]
        [TestCategory("Integraiton.Themes.DeleteVerbose")]
        public void DeleteRangeOf10000ThemesFromDatabaseVerbose()
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

            UserRepository.Create(user).FormatHtml().Output(TestContext);
            user = UserRepository.GetUser(user.Id);

            if(user == null)
                Assert.Fail("User does not exist");

            List<Theme> themes = ReturnSeededListOfThemes(count: 10000, userId: user.Id);
            ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);

            Assert.AreEqual(10000, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            ThemeRepository.DeleteRangeVerbose(themes).FormatHtml().Output(TestContext);
            Assert.AreEqual(0, ThemeRepository.GetThemes().Where(t => t.UserId.Equals(user.Id)).Count());

            if(HasThemes)
                Assert.AreNotEqual(0, ThemeRepository.GetThemes().Count());
        }
        #endregion

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

                UserRepository.Create(user).FormatHtml().Output(TestContext);
                user = UserRepository.GetUser(user.Id);

                if(user == null)
                    Assert.Fail("User does not exist");

                List<Theme> themes = ReturnSeededListOfThemes(count: 10, userId: user.Id);
                ThemeRepository.CreateRange(themes).FormatHtml().Output(TestContext);
            }

            ThemeRepository.DeleteRange(ThemeRepository.GetThemes().ToList()).FormatHtml().Output(TestContext);

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

                UserRepository.Create(user).FormatHtml().Output(TestContext);
                user = UserRepository.GetUser(user.Id);

                if(user == null)
                    Assert.Fail("User does not exist");

                List<Theme> themes = ReturnSeededListOfThemes(count: 10, userId: user.Id);
                ThemeRepository.CreateRangeVerbose(themes).FormatHtml().Output(TestContext);
            }

            ThemeRepository.DeleteRangeVerbose(ThemeRepository.GetThemes().ToList()).FormatHtml().Output(TestContext);

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
                    Name = "Theme from Range - " + (i + 1),
                    UserId = userId ?? 0
                };

                themes.Add(newTheme);
            }

            return themes;
        }
    }
}
