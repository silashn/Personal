using Microsoft.AspNetCore.Mvc;
using Playground.Data.Models;
using Playground.Data.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "*")]
        [Remote("Unique", "CategoryValidator", ErrorMessage = "A Category with that name already exists")]
        public string Name { get; set; }
    }
}
