using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories.Membership;
using Playground.Web.ViewModels.Membership;
using System;

namespace Playground.Web.Controllers.Admin
{
    [Route("Admin/Membership")]
    public class AdminMembershipController : Controller
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly IEmployeeRepository EmployeeRepository;
        public readonly IAuthorRepository AuthorRepository;

        public AdminMembershipController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            EmployeeRepository = (IEmployeeRepository)serviceProvider.GetService(typeof(IEmployeeRepository));
            AuthorRepository = (IAuthorRepository)serviceProvider.GetService(typeof(IAuthorRepository));
        }

        public IActionResult Membership()
        {
            return View(EmployeeRepository.GetEmployees());
        }
        [Route("Authors")]
        public IActionResult Authors(int? id, string response)
        {
            AuthorViewModel model = new AuthorViewModel();

            if(id != null)
            {
                Author author = AuthorRepository.GetAuthor(Convert.ToInt32(id));
                if(author == null)
                {
                    return RedirectToAction("Authors", "AdminMembership");
                }

                switch(response)
                {
                    case "Edit":
                        {
                            model = LoadAuthorModel(model, author);
                        }
                        break;

                    case "Delete":
                        {
                            model.SystemMessage = AuthorRepository.Delete(author);
                            model = LoadAuthorModel(model);
                            TempData["SystemMessage"] = model.SystemMessage;
                            return RedirectToAction("Authors", "AdminMembership");
                        }

                    default:
                        {
                            return RedirectToAction("Authors", "AdminMembership");
                        }
                }
            }
            else
            {
                if(response != null)
                    return RedirectToAction("Authors", "AdminMembership");

                model = LoadAuthorModel(model);
            }
            return View(model);
        }

        [Route("Authors")]
        [HttpPost]
        public IActionResult Authors(AuthorViewModel model)
        {
            if(ModelState.IsValid)
            {
                if(model.Id != null)
                {
                    Author author = AuthorRepository.GetAuthor(Convert.ToInt32(model.Id));
                    author.Name = model.Name;
                    model.SystemMessage = AuthorRepository.Update(author);
                    model = LoadAuthorModel(model, author);
                }
                else
                {
                    Author author = new Author()
                    {
                        Name = model.Name
                    };

                    model.SystemMessage = AuthorRepository.Create(author);
                    model = LoadAuthorModel(model);
                }

                ModelState.Clear();

                TempData["SystemMessage"] = model.SystemMessage;

                return RedirectToAction("Authors", "AdminMembership");
            }
            if(model.Id != null)
            {
                Author author = AuthorRepository.GetAuthor(Convert.ToInt32(model.Id));

                model = LoadAuthorModel(model, author);
            }
            else
            {
                model = LoadAuthorModel(model);
            }

            return View(model);
        }

        [NonAction]
        public AuthorViewModel LoadAuthorModel(AuthorViewModel model, Author author)
        {
            model.Id = author.Id;
            model.Name = author.Name;
            model.Authors = AuthorRepository.GetAuthors();
            return model;
        }

        [NonAction]
        public AuthorViewModel LoadAuthorModel(AuthorViewModel model)
        {
            model.Authors = AuthorRepository.GetAuthors();
            return model;
        }
    }
}
