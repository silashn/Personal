using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pages.Data.Scaffolding.Models
{
    public partial class Themes
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Themes")]
        public virtual Users User { get; set; }
    }
}
