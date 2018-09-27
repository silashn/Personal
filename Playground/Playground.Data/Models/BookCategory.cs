﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Data.Models
{
    public class BookCategory
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Category Category { get; set; }
    }
}
