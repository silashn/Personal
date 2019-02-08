using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pages.Data.Scaffolding.Models
{
    public partial class Users
    {
        private string _name;

        public Users()
        {
            Themes = new HashSet<Themes>();
        }
        [Editable(false)]
        public int Id { get; set; }

        [Required]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if(value.Length > 64)
                {
                    throw new InvalidOperationException("Name cannot be over 64 characters.");
                }

                _name = value;
            }
        }
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
