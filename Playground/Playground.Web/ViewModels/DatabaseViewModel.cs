﻿using Playground.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Playground.Web.ViewModels
{
    public class DatabaseViewModel
    {
        public List<Author> Authors { get; set; }
        public List<Book> Books { get; set; }
        public List<Category> Categories { get; set; }

        #region Author
        public AuthorViewModel Author { get; set; }
        #endregion

        #region Book
        public BookViewModel Book { get; set; }
        #endregion

        #region Category
        public CategoryViewModel Category { get; set; }
        #endregion

        #region Extra
        public string SystemMessage { get; set; }
        public List<CheckBox> CheckBoxList { get; set; }
        #endregion
    }

    public class CheckBox
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
    }
}
