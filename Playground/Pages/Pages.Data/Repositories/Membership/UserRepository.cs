using Pages.Data.Repositories.Interfaces;
using Pages.Data.Scaffolding.Contexts;
using Pages.Data.Scaffolding.Models;
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

        List<Users> IUserRepository.GetUsers()
        {
            return db.Users.ToList();
        }
    }
}
