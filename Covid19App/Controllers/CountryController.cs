using System;
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

            ViewBag.Continents = new SelectList(_context.Continents, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Country country)
        {
            if (CountryExists(country))
            {
                ViewBag.Continents = new SelectList(_context.Continents, "Id", "Name");
                ModelState.AddModelError(string.Empty,
                    $"Das Land {country.Name} / {country.IsoCode} existiert bereits.");
                return View(country);
            }

            if (!ModelState.IsValid)
            {
                return View(country);
            }

            _context.Add(country);
            _context.SaveChanges();
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
        public IActionResult Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (CountryExists(country))
            {
                ViewBag.Continents = new SelectList(_context.Continents, "Id", "Name");
                ModelState.AddModelError(string.Empty,
                    $"Das Land {country.Name} / {country.IsoCode} existiert bereits.");
                return View(country);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Update(country);
            _context.SaveChanges();
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries.Include(x => x.Continent).ToListAsync());
        }

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

        private bool CountryExists(Country country)
        {
            return _context.Countries.Any(e =>
                e.Id != country.Id && (country.Name == e.Name || country.IsoCode == e.IsoCode));
        }
        
    }
}