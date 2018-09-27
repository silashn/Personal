using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
