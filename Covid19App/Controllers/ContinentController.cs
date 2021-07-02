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
        public IActionResult Create(Continent continent)
        {
            if (ContinentNameExists(continent))
            {
                ModelState.AddModelError(string.Empty, $"Der Name {continent.Name} existiert bereits.");
                return View(continent);
            }
            if (!ModelState.IsValid)
            {
                return View(continent);
            }
            _context.Add(continent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = _context.Continents.Find(id);
            if (continent == null)
            {
                return NotFound();
            }
            return View(continent);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Continent continent)
        {
            if (id != continent.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(continent);
            }
            
            if (ContinentNameExists(continent))
            {
                ModelState.AddModelError(string.Empty, $"Der Name {continent.Name} existiert bereits.");
                return View(continent);
            }
            
            try
            {
                _context.Update(continent);
                _context.SaveChanges();
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
        
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = _context.Continents.FirstOrDefault(m => m.Id == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var continent = _context.Continents.Find(id);
            _context.Continents.Remove(continent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ContinentsExists(int id)
        {
            return _context.Continents.Any(e => e.Id == id);
        }

        public IActionResult Index()
        {
            return View(_context.Continents.ToList());
        }
        
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var continent = _context.Continents.FirstOrDefault(m => m.Id == id);
            if (continent == null)
            {
                return NotFound();
            }

            return View(continent);
        }
        
        private bool ContinentNameExists(Continent continent)
        {
            return _context.Continents.Any(e => e.Name == continent.Name && e.Id != continent.Id);
        }
        
    }
}