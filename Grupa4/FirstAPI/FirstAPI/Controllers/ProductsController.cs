using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstAPI.Models.EF;
using Microsoft.Extensions.Logging;

namespace FirstAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Products")]
    public class ProductsController : Controller
    {
        private readonly korporowContext _context;
        private readonly ILogger _logger;
        string logText = "produktu lub serwisu";

        public ProductsController(ILogger<ProductsController> logger, korporowContext context)
        {
            _logger = logger;
            _context = context;
           
        }

        /// <summary>
        /// Pobranie produktów i serwisów
        /// </summary>
        /// <returns>Lista produktów i serwisów</returns>
        // GET: api/Products
        [HttpGet]
        public IEnumerable<Products> GetProducts()
        {
            _logger.LogInformation("Pobranie listy produktów i serwisów");
            return _context.Products;
        }

        /// <summary>
        /// Pobranie produktu lub serwisu
        /// </summary>
        /// <param name="id">Podanie id produktu</param>
        /// <returns>Szczegóły produktu lub serwisu</returns>
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts([FromRoute] int id)
        {
            _logger.LogInformation($"Uruchomiono pobranie {logText} o ID: {id}");
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Błąd walidacji modelu danych {logText} podczas pobrania dla ID: {id}");
                return BadRequest(ModelState);
            }

            var products = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);

            if (products == null)
            {
                _logger.LogError($"Błąd pobrania danych {logText} o ID: {id}");
                return NotFound();
            }

            return Ok(products);
        }

        /// <summary>
        /// Aktualizacja produktu lub serwisu
        /// </summary>
        /// <param name="id">Przekazanie id produktu lub serwisu</param>
        /// <param name="products">Przekazanie zaktualizowanych danych produktu lub serwisu</param>
        /// <returns></returns>
        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts([FromRoute] int id, [FromBody] Products products)
        {
            _logger.LogInformation($"Uruchomiono aktualizację {logText} o ID: {id}");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Błąd walidacji modelu danych {logText} podczas aktualizacji");
                return BadRequest(ModelState);
            }

            if (id != products.Id)
            {
                _logger.LogError($"Błąd dla wybranego {logText} o ID: {id}");
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogError($"Błąd zapisu do bazy dla wybranego {logText} o ID: {id}");
                if (!ProductsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Dodanie produktu lub serwisu
        /// </summary>
        /// <param name="products">Przekazanie id produktu lub serwisu</param>
        /// <returns>Zwraca inforamcje o dodanym produkcie</returns>
        // POST: api/Products
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] Products products)
        {
            _logger.LogInformation($"Uruchomiono dodanie {logText}");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Błąd walidacji modelu danych {logText}");
                return BadRequest(ModelState);
            }

            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }

        /// <summary>
        /// Usunięcie produktu lub serwisu
        /// </summary>
        /// <param name="id">Przekazanie id produktu lub serwisu</param>
        /// <returns></returns>
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            _logger.LogInformation($"Uruchomiono usunięcie {logText} o ID: {id}");
            if (!ModelState.IsValid)
            {
                _logger.LogWarning($"Błąd walidacji modelu danych {logText} podczas usunięcia");
                return BadRequest(ModelState);
            }

            var products = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                _logger.LogError($"Błąd usunięcia bazy wybranego {logText} o ID: {id}");
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return Ok(products);
        }

        /// <summary>
        /// Sprawdza istnienie produktu lub serwisu
        /// </summary>
        /// <param name="id">Podanie ID produktu lub serwisu</param>
        /// <returns></returns>
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}