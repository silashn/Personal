﻿using Pages.Data.Scaffolding.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pages.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Users GetUser(int id);
        IEnumerable<Users> GetUsers();
        string Create(Users user);
        string Update(Users user);
        string Delete(Users user);
    }
}
