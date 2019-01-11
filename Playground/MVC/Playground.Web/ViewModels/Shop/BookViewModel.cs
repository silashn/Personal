using Playground.Data.Models;
using Playground.Data.Models.Elements;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels.Shop
{
    public class BookViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }

        public List<CheckBox> CheckBoxList_Authors { get; set; }
        public List<CheckBox> CheckBoxList_Categories { get; set; }
        public string SystemMessage { get; set; }
    }
}
