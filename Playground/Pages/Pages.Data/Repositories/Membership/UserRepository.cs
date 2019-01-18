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
        public Exception Create(Users user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return null;
            }
            catch(Exception e)
            {
                return e;
            }
        }
    }
}
