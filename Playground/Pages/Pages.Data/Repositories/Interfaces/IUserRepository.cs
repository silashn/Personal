using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetUsers();
        string Create(User user);
        string Update(User user);
        string Delete(User user);
    }
}
