﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using School3.Data;
using School3.Models;






namespace School3.Controllers
{
    public class NauczycieleController : Controller
    {
        private readonly NauczycielContext _context;
        private readonly PrzedmiotContext _db;


        public NauczycieleController(NauczycielContext context, PrzedmiotContext db)
        {
            _context = context;
            _db = db;
        }

        // GET: Nauczyciele
        public async Task<IActionResult> Index()
        //public IActionResult Index()
        {
            
            //var nauczyciele = _context.Nauczyciel.Include(c => c.PrzedmiotId);
            //return View(nauczyciele.ToList());
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
            PrzedmiotyDropDownList();
            return View();
        }

        // POST: Nauczyciele/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Imie,Nazwisko,PrzedmiotId")] Nauczyciel nauczyciel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(nauczyciel);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PrzedmiotyDropDownList(nauczyciel.PrzedmiotId);
            return View(nauczyciel);

            //##################################################################
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
            PrzedmiotyDropDownList(nauczyciel.PrzedmiotId);
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
            PrzedmiotyDropDownList(nauczyciel.PrzedmiotId);
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


        public void PrzedmiotyDropDownList(object selectedPrzedmiot = null)
        {
     
            var przedmiotyQuery = from d in _db.Przedmiot
                                  orderby d.Nazwa
                                  select d;
            ViewBag.PrzedmiotId = new SelectList(przedmiotyQuery, "id", "Nazwa", selectedPrzedmiot);
        }

    }




}
