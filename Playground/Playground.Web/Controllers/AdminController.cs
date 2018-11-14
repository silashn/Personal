using Microsoft.AspNetCore.Mvc;
using Playground.Data.Repositories;
using Playground.Data.Repositories.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEmployeeRepository EmployeeRepository;

        private readonly IServiceProvider ServiceProvider;
        public AdminController(IServiceProvider serviceProvider)
        {
            EmployeeRepository = (IEmployeeRepository)serviceProvider.GetService(typeof(IEmployeeRepository));
            ServiceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View(EmployeeRepository.GetEmployees());
        }
    }
}
