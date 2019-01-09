using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Models.Elements;
using Playground.Data.Repositories.Membership;
using Playground.Data.Repositories.Shop;
using Playground.Web.ViewModels.Shop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Playground.Web.Controllers.Admin
{
    [Route("Admin/Shop")]
    public class AdminShopController : Controller
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly ICategoryRepository CategoryRepository;
        private readonly IBookRepository BookRepository;
        private readonly IAuthorRepository AuthorRepository;
        public AdminShopController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            CategoryRepository = (ICategoryRepository)serviceProvider.GetService(typeof(ICategoryRepository));
            BookRepository = (IBookRepository)serviceProvider.GetService(typeof(IBookRepository));
            AuthorRepository = (IAuthorRepository)serviceProvider.GetService(typeof(IAuthorRepository));
        }

        public IActionResult Shop()
        {
            return View();
        }

        [Route("Books")]
        public IActionResult Books(int? id, string response)
        {
            BookViewModel model = new BookViewModel();

            if(id != null)
            {
                Book book = BookRepository.GetBook(Convert.ToInt32(id));
                if(book == null)
                    return RedirectToAction("Books", "AdminShop");

                switch(response)
                {
                    case "Edit":
                    {
                        model = LoadBookModel(model, book);
                    }
                    break;
                    case "Delete":
                    {
                        TempData["SystemMessage"] = BookRepository.Delete(book);
                        model = LoadBookModel(model);
                        return RedirectToAction("Books", "AdminShop");
                    }
                    default:
                        return RedirectToAction("Books", "AdminShop");
                }
            }
            else
            {
                if(response != null)
                    return RedirectToAction("Books", "AdminShop");

                model = LoadBookModel(model);
            }
            return View(model);
        }

        [Route("Books")]
        [HttpPost]
        public IActionResult Books(BookViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.Id != null)
                {
                    Book book = BookRepository.GetBook(Convert.ToInt32(model.Id));
                    if(book == null)
                        return RedirectToAction("Books", "AdminShop");

                    book.Title = model.Title;
                    book.Description = model.Description;

                    foreach(CheckBox bookAuthor in model.CheckBoxList_Authors.Where(cbl_a => cbl_a.Checked && !book.BookAuthors.Select(ba => ba.AuthorId).Contains(cbl_a.Id)))
                        book.BookAuthors.Add(new BookAuthor() { AuthorId = bookAuthor.Id, BookId = book.Id });

                    foreach(CheckBox bookCategory in model.CheckBoxList_Categories.Where(cbl_c => cbl_c.Checked && !book.BookCategories.Select(bc => bc.CategoryId).Contains(cbl_c.Id)))
                        book.BookCategories.Add(new BookCategory() { CategoryId = bookCategory.Id, BookId = book.Id });

                    foreach(CheckBox bookAuthor in model.CheckBoxList_Authors.Where(cbl_a => !cbl_a.Checked && book.BookAuthors.Select(ba => ba.AuthorId).Contains(cbl_a.Id)))
                        book.BookAuthors.Remove(book.BookAuthors.FirstOrDefault(ba => ba.AuthorId.Equals(bookAuthor.Id)));

                    foreach(CheckBox bookCategory in model.CheckBoxList_Categories.Where(cbl_c => !cbl_c.Checked && book.BookCategories.Select(bc => bc.CategoryId).Contains(cbl_c.Id)))
                        book.BookCategories.Remove(book.BookCategories.FirstOrDefault(bc => bc.CategoryId.Equals(bookCategory.Id)));

                    model.SystemMessage = BookRepository.Update(book);
                    model = LoadBookModel(model, book);
                }
                else
                {
                    Book book = new Book()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        BookAuthors = new List<BookAuthor>(),
                        BookCategories = new List<BookCategory>()
                    };

                    foreach(CheckBox bookAuthor in model.CheckBoxList_Authors.Where(cbl_a => cbl_a.Checked))
                        book.BookAuthors.Add(new BookAuthor() { AuthorId = bookAuthor.Id, BookId = book.Id });

                    foreach(CheckBox bookCategory in model.CheckBoxList_Categories.Where(cbl_c => cbl_c.Checked))
                        book.BookCategories.Add(new BookCategory() { CategoryId = bookCategory.Id, BookId = book.Id });

                    model.SystemMessage = BookRepository.Create(book);
                    model = LoadBookModel(model);
                }

                TempData["SystemMessage"] = model.SystemMessage;

                return RedirectToAction("Books", "AdminShop");
            }

            ModelState.Clear();

            if(model.Id != null)
            {
                Book book = BookRepository.GetBook(Convert.ToInt32(model.Id));

                model = LoadBookModel(model, book);
            }
            else
            {
                model = LoadBookModel(model);
            }

            return View(model);
        }

        [Route("Categories")]
        public IActionResult Categories(int? id, string response)
        {
            CategoryViewModel model = new CategoryViewModel();

            if(id != null)
            {
                Category category = CategoryRepository.GetCategory(Convert.ToInt32(id));
                if(category == null)
                    return RedirectToAction("Categories", "AdminShop");

                switch(response)
                {
                    case "Edit":
                    {
                        model = LoadCategoryModel(model, category);
                    }
                    break;
                    case "Delete":
                    {
                        TempData["SystemMessage"] = CategoryRepository.Delete(category);
                        model = LoadCategoryModel(model);
                        return RedirectToAction("Categories", "AdminShop");
                    }
                    default:
                        return RedirectToAction("Categories", "AdminShop");
                }
            }
            else
            {
                if(response != null)
                    return RedirectToAction("Categories", "AdminShop");

                model = LoadCategoryModel(model);
            }
            return View(model);
        }

        [Route("Categories")]
        [HttpPost]
        public IActionResult Categories(CategoryViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.Id != null)
                {
                    Category category = CategoryRepository.GetCategory(Convert.ToInt32(model.Id));
                    if(category == null)
                        return RedirectToAction("Categories", "AdminShop");

                    category.Name = model.Name;
                    model.SystemMessage = CategoryRepository.Update(category);
                    model = LoadCategoryModel(model);
                }
                else
                {
                    Category category = new Category()
                    {
                        Name = model.Name
                    };
                    model.SystemMessage = CategoryRepository.Create(category);
                    model = LoadCategoryModel(model);
                }

                TempData["SystemMessage"] = model.SystemMessage;

                return RedirectToAction("Categories", "AdminShop");
            }

            ModelState.Clear();

            if(model.Id != null)
            {
                Category category = CategoryRepository.GetCategory(Convert.ToInt32(model.Id));

                model = LoadCategoryModel(model, category);
            }
            else
            {
                model = LoadCategoryModel(model);
            }

            return View(model);
        }

        [NonAction]
        public CategoryViewModel LoadCategoryModel(CategoryViewModel model, Category category)
        {
            model.Id = category.Id;
            model.Name = category.Name;
            model.Categories = CategoryRepository.GetCategories();
            return model;
        }

        [NonAction]
        public CategoryViewModel LoadCategoryModel(CategoryViewModel model)
        {
            model.Categories = CategoryRepository.GetCategories();
            return model;
        }

        [NonAction]
        private BookViewModel LoadBookModel(BookViewModel model, Book book)
        {
            model.Id = book.Id;
            model.Title = book.Title;
            model.Description = book.Description;
            model.Books = BookRepository.GetBooks();
            model.CheckBoxList_Authors = new List<CheckBox>();
            model.CheckBoxList_Categories = new List<CheckBox>();

            foreach(Author author in AuthorRepository.GetAuthors())
                model.CheckBoxList_Authors.Add(new CheckBox() { Checked = book.BookAuthors.Where(ba => ba.AuthorId.Equals(author.Id)).ToList().Count > 0, Id = author.Id, Text = author.Name });
            foreach(Category category in CategoryRepository.GetCategories())
                model.CheckBoxList_Categories.Add(new CheckBox() { Checked = book.BookCategories.Where(bc => bc.CategoryId.Equals(category.Id)).ToList().Count > 0, Id = category.Id, Text = category.Name });

            return model;
        }

        [NonAction]
        private BookViewModel LoadBookModel(BookViewModel model)
        {
            model.Books = BookRepository.GetBooks();
            model.CheckBoxList_Authors = new List<CheckBox>();
            model.CheckBoxList_Categories = new List<CheckBox>();

            foreach(Author author in AuthorRepository.GetAuthors())
                model.CheckBoxList_Authors.Add(new CheckBox() { Checked = false, Id = author.Id, Text = author.Name });
            foreach(Category category in CategoryRepository.GetCategories())
                model.CheckBoxList_Categories.Add(new CheckBox() { Checked = false, Id = category.Id, Text = category.Name });

            return model;
        }
    }
}
