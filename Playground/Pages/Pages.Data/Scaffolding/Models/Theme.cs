using Pages.Data.Repositories.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Pages.Data.Scaffolding.Models
{
    public partial class Theme
    {
        private string _name;
        private string _color;

        public int Id { get; set; }
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
                    throw new InvalidOperationException("Name cannot exceed 64 characters.");
                }
                else if(value.Length == 0)
                {
                    throw new InvalidOperationException("Name must be entered");
                }

                _name = value;
            }
        }
        [Required]
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                if(System.Text.RegularExpressions.Regex.IsMatch(value, @"\A\b[0-9a-fA-F]+\b\Z") && (value.Length == 0 || value.Length == 3 || value.Length == 6))
                {
                    _color = value;
                }
                else
                {
                    throw new InvalidOperationException("Color is not of correct format");
                }
            }
        }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Themes")]
        public virtual User User { get; set; }
    }
}
