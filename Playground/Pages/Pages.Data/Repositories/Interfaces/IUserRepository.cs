using Pages.Data.Models.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUser(int id);
        List<User> GetUsers();
    }
}
