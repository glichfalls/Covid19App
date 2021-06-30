using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid19App.Models
{
    public class Report
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Das Datum ist erforderlich!")]
        [Display(Name = "Datum")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required(ErrorMessage = "Die Anzahl neuer Fälle ist erforderlich!")]
        [Display(Name = "Neue Fälle")]
        public int Cases { get; set; }
        
        [Required(ErrorMessage = "Die Anzahl Todesopfer ist erforderlich!")]
        [Display(Name = "Neue Todesopfer")]
        public int Deaths { get; set; }
        
        [Required(ErrorMessage = "Die Anzahl Tests ist erforderlich!")]
        [Display(Name = "Anzahl Tests")]
        public int Tests { get; set; }
        
        [Required(ErrorMessage = "Die Anzahl Impfungen ist erforderlich!")]
        [Display(Name = "Impfungen")]
        public int Vaccinations { get; set; }
        
        [Required(ErrorMessage = "Der R-Wert ist erfoderlich!")]
        [Display(Name = "Reproduktionszahl (R-Wert)")]
        public float ReproductionRate { get; set; }
        
        [Display(Name = "Land")]
        [Required(ErrorMessage = "Das Land ist erforderlich!")]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }
        
        public int CountryId { get; set; }
    }
}