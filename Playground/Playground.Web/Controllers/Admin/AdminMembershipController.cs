using Microsoft.AspNetCore.Mvc;
using Playground.Data.Repositories.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Controllers.Admin
{
    [Route("Admin/Membership")]
    public class AdminMembershipController : Controller
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly IEmployeeRepository EmployeeRepository;

        public AdminMembershipController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            EmployeeRepository = (IEmployeeRepository)serviceProvider.GetService(typeof(IEmployeeRepository));
        }

        public IActionResult Membership()
        {
            return View(EmployeeRepository.GetEmployees());
        }
    }
}
