using Pages.Data.Clients;
using Pages.Data.Contexts;
using Pages.Data.Models.Membership;
using Pages.Data.Repositories.Interfaces;
using Pages.Settings.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Repositories.Membership
{
    public class UserRepository : DatabaseClient, IUserRepository
    {
        public UserRepository(DatabaseSettings settings) : base(settings)
        {

        }

        public User GetUser(int id)
        {
            using(PagesDbContext db = new PagesDbContext(settings))
            {
                return db.Users.FirstOrDefault(u => u.Id.Equals(id));
            }
        }

        List<User> IUserRepository.GetUsers()
        {
            using(PagesDbContext db = new PagesDbContext(settings))
            {
                return db.Users.ToList();
            }
        }
    }
}
