using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCarRental.DAL;
using WebAppCarRental.Models;

namespace WebAppCarRental.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarRentalContext _context;

        public CarsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["MarkaSortParm"] = String.IsNullOrEmpty(sortOrder) ? "marka_desc" : "";
            ViewData["TypSortParm"] = sortOrder == "Typ" ? "Typ_desc" : "Typ";
            ViewData["RokProdukcjiSortParm"] = sortOrder == "RokProdukcji" ? "RokProdukcji_desc" : "RokProdukcji";
            ViewData["WyposazenieSortParm"] = sortOrder == "Wyposazenie" ? "Wyposazenie_desc" : "Wyposazenie";
            ViewData["CenaZaDzienSortParm"] = sortOrder == "CenaZaDzien" ? "CenaZaDzien_desc" : "CenaZaDzien";
            ViewData["SpalanieSortParm"] = sortOrder == "Spalanie" ? "Spalanie_desc" : "Spalanie";

            ViewData["CurrentFilter"] = searchString;

            var cars = from c in _context.Cars
                       select c;

            if (!String.IsNullOrEmpty(searchString))
            {
                cars = cars.Where(c => c.Marka.Contains(searchString)
                                       || c.Typ.Contains(searchString)
                                       || c.Wyposazenie.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "marka_desc":
                    cars = cars.OrderByDescending(s => s.Marka);
                    break;
                case "Typ":
                    cars = cars.OrderBy(s => s.Typ);
                    break;
                case "Typ_desc":
                    cars = cars.OrderByDescending(s => s.Typ);
                    break;
                case "RokProdukcji":
                    cars = cars.OrderBy(s => s.RokProdukcji);
                    break;
                case "RokProdukcji_desc":
                    cars = cars.OrderByDescending(s => s.RokProdukcji);
                    break;
                case "Wyposazenie":
                    cars = cars.OrderBy(s => s.Wyposazenie);
                    break;
                case "Wyposazenie_desc":
                    cars = cars.OrderByDescending(s => s.Wyposazenie);
                    break;
                case "CenaZaDzien":
                    cars = cars.OrderBy(s => s.CenaZaDzien);
                    break;
                case "CenaZaDzien_desc":
                    cars = cars.OrderByDescending(s => s.CenaZaDzien);
                    break;
                case "Spalanie":
                    cars = cars.OrderBy(s => s.Spalanie);
                    break;
                case "Spalanie_desc":
                    cars = cars.OrderByDescending(s => s.Spalanie);
                    break;
                default:
                    cars = cars.OrderBy(s => s.Marka);
                    break;
            }
            return View(await cars.AsNoTracking().ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marka,Typ,RokProdukcji,Wyposazenie,CenaZaDzien,Spalanie")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }


        //public void Duplicate(int? id)
        //{
        //    if (id == null)
        //    {
        //        var car = _context.Cars.SingleOrDefaultAsync(m => m.Id == id);

        //        if (car != null)
        //        {
        //            _context.Add(car);
        //        }
        //    }

        //}

        // GET: Cars/Find/MS
        public async Task<IActionResult> Filter(string str)
        {
            if (str == null)
            {
                return NotFound();
            }

            var cars = _context.Cars.Where(m => m.Marka.Contains(str));
            if (cars == null)
            {
                return NotFound();
            }
            return View(cars);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marka,Typ,RokProdukcji,Wyposazenie,CenaZaDzien,Spalanie")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .SingleOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(m => m.Id == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.Id == id);
        }
    }
}
