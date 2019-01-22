using Pages.Data.Scaffolding.Models;
using System;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int id);
        IQueryable<Users> GetUsers();
        string Create(Users user);
        string Update(Users user);
    }
}
