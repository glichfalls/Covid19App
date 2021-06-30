using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspPersonenverwaltung.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Der Name des Landes ist erforderlich!")]
        [StringLength(100, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Name { get; set; }
        
        [Display(Name = "ISO Code")]
        [Required(ErrorMessage = "Der Länder ISO Code ist erforderlich!")]
        public string IsoCode { get; set; }
        
        [Display(Name = "Anzahl Einwohner")]
        [Required(ErrorMessage = "Die Anzahl Einwohner ist erfoderlich!")]
        public int Population { get; set; }
        
        [Display(Name = "Kontinent")]
        [Required(ErrorMessage = "Der Kontinent ist erforderlich!")]
        [ForeignKey("ContinentId")]
        public virtual Continent Continent { get; set; }
        
        public int ContinentId { get; set; }
    }
}