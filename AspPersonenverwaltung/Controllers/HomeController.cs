using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AspPersonenverwaltung.Data;
using Microsoft.AspNetCore.Mvc;
using AspPersonenverwaltung.Models;
using Microsoft.EntityFrameworkCore;

namespace AspPersonenverwaltung.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports.Include(x=>x.Country).ToListAsync());
        }

        public IActionResult Statistics()
        {
            if (!_context.Continents.Any() || !_context.Countries.Any() || !_context.Reports.Any())
            {
                return RedirectToAction(nameof(Error));
            }

            var reportList = _context.Reports.Include(x => x.Country).ToList();
            var statisticList = new StatisticsViewModel();
            
            return View(statisticList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
