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
    public class UczniowieController : Controller
    {
        private readonly UczenContext _context;

        public UczniowieController(UczenContext context)
        {
            _context = context;
        }

        // GET: Uczniowie
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uczen.ToListAsync());
        }

        // GET: Uczniowie/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .SingleOrDefaultAsync(m => m.Imie == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // GET: Uczniowie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uczniowie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Imie,Nazwisko,Rocznik,Klasa")] Uczen uczen)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uczen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uczen);
        }

        // GET: Uczniowie/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen.SingleOrDefaultAsync(m => m.Imie == id);
            if (uczen == null)
            {
                return NotFound();
            }
            return View(uczen);
        }

        // POST: Uczniowie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Imie,Nazwisko,Rocznik,Klasa")] Uczen uczen)
        {
            if (id != uczen.Imie)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uczen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UczenExists(uczen.Imie))
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
            return View(uczen);
        }

        // GET: Uczniowie/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uczen = await _context.Uczen
                .SingleOrDefaultAsync(m => m.Imie == id);
            if (uczen == null)
            {
                return NotFound();
            }

            return View(uczen);
        }

        // POST: Uczniowie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var uczen = await _context.Uczen.SingleOrDefaultAsync(m => m.Imie == id);
            _context.Uczen.Remove(uczen);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UczenExists(string id)
        {
            return _context.Uczen.Any(e => e.Imie == id);
        }
    }
}
