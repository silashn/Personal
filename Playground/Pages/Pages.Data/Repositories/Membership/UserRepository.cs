using Microsoft.EntityFrameworkCore;
using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
using System;
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
            catch (Exception e)
            {
                return "<p class='error'>Could not create user '" + user.Name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br />" + e.InnerException + "</p>" : "");
            }
        }

        public string Update(Users user)
        {
            string name = user.Name;

            try
            {
                Users UpdateUser = GetUser(user.Id);

                UpdateUser.Email = user.Email;
                UpdateUser.Name = user.Name;
                UpdateUser.Password = user.Password;
                UpdateUser.Themes = user.Themes;

                db.Attach(UpdateUser).State = EntityState.Modified;
                db.SaveChanges();
                return "<p class='success'>Successfully updated author '" + name + "'.</p>";
            }
            catch (Exception e)
            {
                return "<p class='error'>Could not update user '" + name + "': " + e.Message + "</p>" + (e.InnerException != null ? "<p class='error inner_exception'><b><i>Inner exception:</i></b><br /><p class='error inner_exception_message'>" + e.InnerException + "</p></p>" : "");
            }
        }
    }
}