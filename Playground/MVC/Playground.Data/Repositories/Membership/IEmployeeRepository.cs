using System.Collections.Generic;
using Playground.Data.Models.Membership;

namespace Playground.Data.Repositories.Membership
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
    }
}