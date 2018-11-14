using Playground.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class AuthorViewModel : BaseViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
    }
}
