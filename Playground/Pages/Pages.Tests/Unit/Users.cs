using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Repositories.Membership;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Tests.Unit
{
    [TestClass]
    [TestCategory("Unit")]
    [TestCategory("Unit.Users")]
    [TestCategory("Users")]
    public class Users
    {
        [TestMethod]
        public void UserNameDoesNotAllowOver64Characters()
        {
            try
            {
                User user = new User()
                {
                    Name = new string('x', 100)
                };
                Assert.Fail("User name allowed over 64 characters.");
            }
            catch(Exception ex)
            {
                if(ex.GetType() == typeof(AssertFailedException))
                    Assert.Fail("User name allowed over 64 characters.");
                else
                    Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
            }
        }

        [TestMethod]
        public void UserNameCannotBeEmpty()
        {
            try
            {
                User user = new User()
                {
                    Name = ""
                };
                Assert.Fail("User name allowed to be empty.");
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