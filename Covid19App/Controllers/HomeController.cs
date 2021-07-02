using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Covid19App.Data;
using Microsoft.AspNetCore.Mvc;
using Covid19App.Models;
using Microsoft.EntityFrameworkCore;

namespace Covid19App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Reports
                .Include(x=> x.Country)
                .ThenInclude(x => x.Continent)
                .ToList()
            );
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
