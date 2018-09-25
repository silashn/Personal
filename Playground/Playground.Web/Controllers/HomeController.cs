using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IBookRepository bookRepository;

        public HomeController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            bookRepository = (IBookRepository)serviceProvider.GetService(typeof(IBookRepository));
        }

        public IActionResult Index()
        {
            List<Book> books = bookRepository.GetBooks().ToList();
            return View(books);
        }
    }
}
