using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
    }
}
