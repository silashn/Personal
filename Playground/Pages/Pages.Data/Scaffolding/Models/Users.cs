using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pages.Data.Scaffolding.Models
{
    public partial class Users
    {
        public Users()
        {
            Themes = new HashSet<Themes>();
        }
        [Editable(false)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<Themes> Themes { get; set; }
    }
}
