using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using System.Linq;

namespace Pages.Tests.Integration
{
    [TestClass]
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
        public void InsertNewThemeIntoDatabase()
        {
            User user = new User()
            {
                Name = "Test User",
                Email = "Test@Email.dk",
                Password = "test123"
            };

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
    }
}
