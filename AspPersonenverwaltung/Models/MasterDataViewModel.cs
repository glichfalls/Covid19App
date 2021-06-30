using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspPersonenverwaltung.Models
{
    public class MasterDataViewModel
    {
        public List<Continent> ContinentList { get; set; }
        public List<Country> CountryList { get; set; }
        public List<Report> ReportList { get; set; }
        
        public MasterDataViewModel()
        {
            ContinentList = new List<Continent>();
            CountryList = new List<Country>();
            ReportList = new List<Report>();
        }
    }
}
