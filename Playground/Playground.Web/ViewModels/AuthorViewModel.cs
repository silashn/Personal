using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class AuthorViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
    }
}
