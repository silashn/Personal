using Pages.Data.Scaffolding.Models;
using System.Collections.Generic;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int id);
        List<Users> GetUsers();
    }
}
