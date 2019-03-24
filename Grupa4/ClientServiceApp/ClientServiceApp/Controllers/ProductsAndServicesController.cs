using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClientServiceApp.Data;
using ClientServiceApp.Models.ProductsAndServices;
using System.Collections;

namespace ClientServiceApp.Controllers
{
    public class ProductsAndServicesController : Controller
    {
        private readonly ProductsAndServicesContext _context;

        public ProductsAndServicesController(ProductsAndServicesContext context)
        {
            _context = context;
        }

        // GET: ProductsAndServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: ProductsAndServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsAndServices = await _context.Products
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productsAndServices == null)
            {
                return NotFound();
            }

            return View(productsAndServices);
        }

        // GET: ProductsAndServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsAndServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Category")] ProductsAndServices productsAndServices)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productsAndServices);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productsAndServices);
        }

        // GET: ProductsAndServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsAndServices = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (productsAndServices == null)
            {
                return NotFound();
            }
            return View(productsAndServices);
        }

        // POST: ProductsAndServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category")] ProductsAndServices productsAndServices)
        {
            if (id != productsAndServices.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productsAndServices);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsAndServicesExists(productsAndServices.Id))
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
            return View(productsAndServices);
        }

        // GET: ProductsAndServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productsAndServices = await _context.Products
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productsAndServices == null)
            {
                return NotFound();
            }

            return View(productsAndServices);
        }

        // POST: ProductsAndServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productsAndServices = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            _context.Products.Remove(productsAndServices);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsAndServicesExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<IActionResult> GetItemByName (string name)
        {
            //List items
            var item = await _context.Products.Where(c => c.Name == name).ToListAsync();
            if (item == null)
                return Content("not found");

            return View(item);
            //return View(await _context.Products.Single<items>);
        }
    }
}
