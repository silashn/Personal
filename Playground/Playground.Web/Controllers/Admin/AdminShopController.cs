using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories.Shop;
using Playground.Web.ViewModels.Shop;
using System;

namespace Playground.Web.Controllers.Admin
{
    [Route("Admin/Shop")]
    public class AdminShopController : Controller
    {
        private readonly IServiceProvider ServiceProvider;
        private readonly ICategoryRepository CategoryRepository;
        public AdminShopController(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;

            CategoryRepository = (ICategoryRepository)serviceProvider.GetService(typeof(ICategoryRepository));
        }

        public IActionResult Shop()
        {
            return View();
        }

        [Route("Books")]
        public IActionResult Books()
        {
            return View();
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
                            model.Name = category.Name;
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
    }
}
