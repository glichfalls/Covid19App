using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Covid19App.Models
{
    public class Continent
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Kontinent")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}