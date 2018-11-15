using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Models.Elements;
using Playground.Data.Repositories.Shop;
using Playground.Data.Repositories.Membership;
using Playground.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Playground.Web.ViewModels.Shop;

namespace Playground.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;

        public HomeController(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            authorRepository = (IAuthorRepository)serviceProvider.GetService(typeof(IAuthorRepository));
            bookRepository = (IBookRepository)serviceProvider.GetService(typeof(IBookRepository));
            categoryRepository = (ICategoryRepository)serviceProvider.GetService(typeof(ICategoryRepository));
        }

        public IActionResult Index(int? id)
        {
            DatabaseViewModel model = new DatabaseViewModel()
            {
                Authors = authorRepository.GetAuthors(),
                Books = bookRepository.GetBooks(),
                Categories = categoryRepository.GetCategories(),
                CheckBoxList = new List<CheckBox>()
            };

            if(id != null)
            {
                Book book = bookRepository.GetBook(Convert.ToInt32(id));
                model.Book = new BookViewModel()
                {
                    Authors = book.BookAuthors.Select(ba => ba.Author).ToList(),
                    Categories = book.BookCategories.Select(bc => bc.Category).ToList(),
                    Description = book.Description,
                    Title = book.Title,
                    Id = id
                };

                foreach(Author author in authorRepository.GetAuthors())
                {
                    model.CheckBoxList.Add(new CheckBox { Text = author.Name, Id = author.Id, Checked = model.Book.Authors.Where(a => a.Id.Equals(author.Id)).ToList().Count > 0 });
                }
            }
            else
            {
                foreach(Author author in authorRepository.GetAuthors())
                {
                    model.CheckBoxList.Add(new CheckBox { Text = author.Name, Id = author.Id });
                }
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(DatabaseViewModel model)
        {
            string message = "";

            if(ModelState.IsValid)
            {
                if(model.Author != null)
                {
                    Author author = new Author()
                    {
                        Name = model.Author.Name
                    };

                    message += authorRepository.Create(author);
                }

                if(model.Category != null)
                {
                    Category category = new Category()
                    {
                        Name = model.Category.Name
                    };

                    message += categoryRepository.Create(category);
                }


                ModelState.Clear();
            }

            model = new DatabaseViewModel()
            {
                Authors = authorRepository.GetAuthors(),
                Books = bookRepository.GetBooks(),
                Categories = categoryRepository.GetCategories(),
                SystemMessage = message
            };

            foreach(Author author in authorRepository.GetAuthors())
            {
                model.CheckBoxList.Add(new CheckBox { Text = author.Name, Id = author.Id });
            }

            return View(model);
        }
    }
}
