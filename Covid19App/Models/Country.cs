using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19App.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Land")]
        [StringLength(100, ErrorMessage = "Max. ist 50 Zeichen.")]
        public string Name { get; set; }
        
        [Display(Name = "ISO Code")]
        public string IsoCode { get; set; }
        
        [Display(Name = "Anzahl Einwohner")]
        public int Population { get; set; }
        
        [Display(Name = "Kontinent")]
        [ForeignKey("ContinentId")]
        public virtual Continent Continent { get; set; }
        
        public int ContinentId { get; set; }
    }
}