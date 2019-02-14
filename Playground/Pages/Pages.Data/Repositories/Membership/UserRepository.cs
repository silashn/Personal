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

        public User GetUser(int id)
        {
            return db.Users.AsNoTracking().Include(u => u.Themes).SingleOrDefault(u => u.Id.Equals(id));
        }

        IEnumerable<User> IUserRepository.GetUsers()
        {
            return db.Users.AsNoTracking().Include(u => u.Themes);
        }

        public string Create(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return "<p class='success'>Successfully created user '" + user.Name + "'.</p>";
            }
            catch (Exception e)
            {
                return "<p class='error'>Could not create user '" + user.Name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(User user)
        {
            string name = user.Name;

            try
            {
                db.Attach(user).State = EntityState.Modified;
                db.SaveChanges();
                return "<p class='success'>Successfully updated user '" + name + "'.</p>";
            }
            catch (Exception e)
            {
                return "<p class='error'>Could not update user '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }

        public string Delete(User user)
        {
            string name = user.Name;

            try
            {
                db.Themes.RemoveRange(user.Themes);
                db.Users.Remove(user);

                db.SaveChanges();
                return "<p class='success'>Successfully deleted user '" + name + "'.</p>";
            }
            catch(Exception e)
            {
                return "<p class='error'>Could not delete user '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }
    }
}