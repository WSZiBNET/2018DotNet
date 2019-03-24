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
    public class PrzedmiotyController : Controller
    {
        private readonly PrzedmiotContext _context;

        public PrzedmiotyController(PrzedmiotContext context)
        {
            _context = context;
        }

        // GET: Przedmioty
        public async Task<IActionResult> Index()
        {
            return View(await _context.Przedmiot.ToListAsync());
        }

        // GET: Przedmioty/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot
                .SingleOrDefaultAsync(m => m.id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // GET: Przedmioty/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Przedmioty/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Nazwa,Opis")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(przedmiot);
        }

        // GET: Przedmioty/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot.SingleOrDefaultAsync(m => m.id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }
            return View(przedmiot);
        }

        // POST: Przedmioty/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Nazwa,Opis")] Przedmiot przedmiot)
        {
            if (id != przedmiot.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotExists(przedmiot.id))
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
            return View(przedmiot);
        }

        // GET: Przedmioty/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot
                .SingleOrDefaultAsync(m => m.id == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // POST: Przedmioty/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var przedmiot = await _context.Przedmiot.SingleOrDefaultAsync(m => m.id == id);
            _context.Przedmiot.Remove(przedmiot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotExists(int id)
        {
            return _context.Przedmiot.Any(e => e.id == id);
        }
    }
}
