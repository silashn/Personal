using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels.Shop
{
    public class CategoryViewModel : BaseViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote("Unique", "CategoryValidator", ErrorMessage = "A Category with that name already exists")]
        public string Name { get; set; }
        public List<Category> Categories { get; set; }
    }
}
