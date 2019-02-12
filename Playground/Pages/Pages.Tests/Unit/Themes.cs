using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Tests.Unit
{
    [TestClass]
    [TestCategory("Unit")]
    [TestCategory("Unit.Themes")]
    [TestCategory("Themes")]
    public class Themes
    {
        [TestMethod]
        public void ThemeNameDoesNotAllowOver64Characters()
        {
            try
            {
                Theme user = new Theme()
                {
                    Name = new string('x', 100)
                };

                Assert.Fail("Theme allowed name above 64 characters.");
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(AssertFailedException))
                    Assert.Fail(ex.Message);
                else
                    Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void ThemeColorOnlyAllows6DigitsHexFormat()
        {
            try
            {
                Theme user = new Theme()
                {
                    Name = "Only Hex Theme Color",
                    Color = "ABCDEFG"
                };

                Assert.Fail("Theme color allowed non-hex format.");
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(AssertFailedException))
                    Assert.Fail(ex.Message);
                else
                    Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }
    }
}
