using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspPersonenverwaltung.Models
{
    public class StatisticsViewModel
    {
        public string Table { get; set; }
        public string Theme { get; set; }
        public int Count { get; set; }

        public List<StatisticsViewModel> StatisticList { get; set; }

        public StatisticsViewModel()
        {
            StatisticList = new List<StatisticsViewModel>();
        }
    }
}
