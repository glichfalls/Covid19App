using System.Threading.Tasks;
using Covid19App.Data;
using Covid19App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Covid19App.Controllers
{
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Create()
        {
            if (!_context.Continents.Any())
            {
                return RedirectToAction("Error", "Home");
            }

            var country = new Country();
            ViewBag.Continents = new SelectList(_context.Continents, "Id", "Name", _context.Continents.FirstOrDefault().Name);
            return View(country);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Country country)
        {
            country.Continent = _context.Continents.FirstOrDefault(x => x.Id == country.Continent.Id);
            if (country.Continent != null)
            {
                country.ContinentId = country.Continent.Id;
            }
            if (!ModelState.IsValid)
            {
                return View(country);
            }
            _context.Add(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return NotFound();
            }
            ViewBag.Continents = new SelectList(_context.Continents, "Id", "Name", country.ContinentId);
            return View(country);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(country);
            }
            
            try
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(country.Id))
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

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.ToListAsync());
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }
    }
}