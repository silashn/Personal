using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pages.Data.Scaffolding.Models
{
    public partial class User
    {
        private string _name;

        public User()
        {
            Themes = new HashSet<Theme>();
        }

        [Editable(false)]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
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
                else if(value.Length == 0)
                {
                    throw new InvalidOperationException("Name must be entered");
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
        public virtual ICollection<Theme> Themes { get; set; }
    }
}
