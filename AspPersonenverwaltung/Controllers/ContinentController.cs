using System.Threading.Tasks;
using Covid19App.Data;
using Covid19App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Covid19App.Controllers
{
    public class ContinentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContinentController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Continent continent)
        {
            if (!ModelState.IsValid || ContinentNameExists(continent.Name))
            {
                return View(continent);
            }
            _context.Add(continent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continents.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }
            return View(continent);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Continent continent)
        {
            if (id != continent.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(continent);
            }
            
            try
            {
                _context.Update(continent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentsExists(continent.Id))
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var continent = await _context.Continents.FindAsync(id);
            _context.Continents.Remove(continent);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContinentsExists(int id)
        {
            return _context.Continents.Any(e => e.Id == id);
        }

        private bool ContinentNameExists(string name)
        {
            return _context.Continents.Any(e => e.Name == name);
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Continents.ToListAsync());
        }
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = await _context.Continents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }
    }
}