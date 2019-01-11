using Playground.Data.Clients;
using Playground.Data.Contexts;
using Playground.Data.Models.Membership;
using Playground.Settings.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Data.Repositories.Membership
{
    public class EmployeeRepository : DatabaseClient, IEmployeeRepository
    {
        public EmployeeRepository(DatabaseSettings settings) : base(settings)
        {

        }

        public List<Employee> GetEmployees()
        {
            using(PlaygroundDbContext db = new PlaygroundDbContext(settings))
            {
                return db.Employees.ToList();
            }
        }
    }
}
