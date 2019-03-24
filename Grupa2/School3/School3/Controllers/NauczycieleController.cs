using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using School3.Data;
using School3.Models;

namespace School3.Controllers
{
    public class NauczycieleController : Controller
    {
        private readonly NauczycielContext _context;

        public NauczycieleController(NauczycielContext context)
        {
            _context = context;
        }

        // GET: Nauczyciele
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nauczyciel.ToListAsync());
        }

        // GET: Nauczyciele/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel
                .SingleOrDefaultAsync(m => m.id == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // GET: Nauczyciele/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nauczyciele/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Imie,Nazwisko,PrzedmiotId")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nauczyciel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nauczyciel);
        }

        // GET: Nauczyciele/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel.SingleOrDefaultAsync(m => m.id == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }
            return View(nauczyciel);
        }

        // POST: Nauczyciele/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Imie,Nazwisko,PrzedmiotId")] Nauczyciel nauczyciel)
        {
            if (id != nauczyciel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nauczyciel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NauczycielExists(nauczyciel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nauczyciel);
        }

        // GET: Nauczyciele/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel
                .SingleOrDefaultAsync(m => m.id == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // POST: Nauczyciele/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nauczyciel = await _context.Nauczyciel.SingleOrDefaultAsync(m => m.id == id);
            _context.Nauczyciel.Remove(nauczyciel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NauczycielExists(int id)
        {
            return _context.Nauczyciel.Any(e => e.id == id);
        }
    }
}
