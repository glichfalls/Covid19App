using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AspPersonenverwaltung.Models
{
    public class Continent
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Kontinent")]
        [Required(ErrorMessage = "Der Name ist erfoderlich")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}