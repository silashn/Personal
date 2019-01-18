using Pages.Data.Scaffolding.Models;
using System;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int id);
        IQueryable<Users> GetUsers();
        Exception Create(Users user);
    }
}
