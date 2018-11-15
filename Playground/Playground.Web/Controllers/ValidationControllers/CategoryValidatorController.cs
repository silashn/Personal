using Microsoft.AspNetCore.Mvc;
using Playground.Data.Repositories.Shop;

namespace Playground.Web.Controllers.ValidationControllers
{
    public class CategoryValidatorController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryValidatorController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        
        public JsonResult Unique(string Name)
        {
            return Json(!categoryRepository.CategoryExists(Name));
        }
    }
}