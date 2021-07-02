using System.Threading.Tasks;
using Covid19App.Data;
using Covid19App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Covid19App.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            if (!_context.Countries.Any())
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.Countries = new SelectList(_context.Countries, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Report report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }
            _context.Add(report);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Report report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(report);
            }

            try
            {
                _context.Update(report);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(report.Id))
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

            var report = _context.Reports.FirstOrDefault(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var report = _context.Reports.Find(id);
            _context.Reports.Remove(report);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }

        public IActionResult Index()
        {
            return View(_context.Reports
                .Include(x => x.Country)
                .ThenInclude(x => x.Continent)
                .ToList()
            );
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = _context.Reports.FirstOrDefault(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }
    }
}