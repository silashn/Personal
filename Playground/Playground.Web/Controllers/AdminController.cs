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
        private readonly IAuthorRepository AuthorRepository;
        private readonly IBookRepository BookRepository;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IEmployeeRepository EmployeeRepository;

        private readonly IServiceProvider ServiceProvider;
        public AdminController(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            AuthorRepository = (IAuthorRepository)serviceProvider.GetService(typeof(IAuthorRepository));
            BookRepository = (IBookRepository)serviceProvider.GetService(typeof(IBookRepository));
            CategoryRepository = (ICategoryRepository)serviceProvider.GetService(typeof(ICategoryRepository));
            EmployeeRepository = (IEmployeeRepository)serviceProvider.GetService(typeof(IEmployeeRepository));
            ServiceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View(EmployeeRepository.GetEmployees());
        }

        [Route("Admin/Shop")]
        public IActionResult Shop()
        {
            return View();
        }

        [Route("Admin/Membership/Authors")]
        public IActionResult Authors()
        {
            return View();
        }

        [Route("Admin/Shop/Books")]
        public IActionResult Books()
        {
            return View();
        }
        
        [Route("Admin/Shop/Categories")]
        public IActionResult Categories()
        {
            return View();
        }
    }
}
