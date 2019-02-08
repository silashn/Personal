using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;

namespace Pages.UnitTests.Models
{
    [TestClass]
    public class UsersTests
    {
        [TestMethod]
        public void AddSingleTheme()
        {
            Users user = new Users()
            {
                Themes = { new Themes { } }
            };

            Assert.IsTrue(user.Themes.Count.Equals(1));
        }

        [TestMethod]
        public void AddMultipleThemes()
        {
            Users user = new Users();
            List<Themes> themes = new List<Themes>();

            for(int i = 0; i < 2; i++)
                themes.Add(new Themes());

            user.Themes = themes;

            Assert.IsTrue(user.Themes.Count > 1);
        }
    }
}
