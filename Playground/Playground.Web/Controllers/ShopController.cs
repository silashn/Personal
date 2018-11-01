using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories;

namespace Playground.Web.Controllers
{
    public class ShopController : Controller
    {
        public IBookRepository BookRepository { get; }

        public ShopController(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Shop";
            return View();
        }

        public IActionResult Books()
        {
            ViewBag.Title = "Shop - Books";
            List<Book> books = BookRepository.GetBooks();
            return View(books);
        }
    }
}