using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories;
using Playground.Web.ViewModels;
using System;
using System.Collections.Generic;

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

        public IActionResult Index()
        {
            DatabaseViewModel model = new DatabaseViewModel()
            {
                Authors = authorRepository.GetAuthors(),
                Books = bookRepository.GetBooks(),
                Categories = categoryRepository.GetCategories()
            };

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
            CheckBox_Author[] CheckBox_Authors;

            foreach(Author author in authorRepository.GetAuthors())
            {
                CheckBox_Authors.Add(new CheckBox_Author { Name = author.Name, Id = author.Id });
            }

            model = new DatabaseViewModel()
            {
                Authors = authorRepository.GetAuthors(),
                Books = bookRepository.GetBooks(),
                Categories = categoryRepository.GetCategories(),
                SystemMessage = message,
                CheckBoxList_Authors = CheckBox_Authors
            };

            return View(model);
        }
    }
}
