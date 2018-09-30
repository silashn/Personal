using Microsoft.AspNetCore.Mvc;
using Playground.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Controllers.ValidationControllers
{
    public class CategoryValidatorController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryValidatorController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public JsonResult Unique(string Name)
        {
            return Json(!categoryRepository.CategoryExists(Name));
        }
    }
}
