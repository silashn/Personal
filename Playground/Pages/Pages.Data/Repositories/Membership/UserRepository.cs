using Microsoft.EntityFrameworkCore;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Data.Repositories.Membership
{
    public class UserRepository : IUserRepository
    {
        private readonly PagesDbContext db;

        public UserRepository(PagesDbContext db)
        {
            this.db = db;
        }


        public Users GetUser(int id)
        {
            return db.Users.FirstOrDefault(u => u.Id.Equals(id));
        }

        IQueryable<Users> IUserRepository.GetUsers()
        {
            return db.Users;
        }

        public string Create(Users user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return "<p class='success'>Successfully created user '" + user.Name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could create user '" + user.Name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Users user)
        {
            try
            {
                db.Attach(user).State = EntityState.Modified;
                db.SaveChanges();
                return "<p class='success'>Successfully deleted author '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not delete author '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }
        }
    }
}
