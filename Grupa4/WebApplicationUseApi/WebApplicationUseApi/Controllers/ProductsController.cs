using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplicationUseApi.Models;

namespace WebApplicationUseApi.Controllers
{
    public class ProductsController : Controller
    {
        string baseUrl = "http://localhost:44323/";
        HttpClient hc = new HttpClient();

        void SetHC()
        {
            hc.BaseAddress = new Uri(baseUrl);
            hc.DefaultRequestHeaders.Clear();
            hc.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int? searchInt)
        {
            SetHC();
            //List<ProductsModel> Products = new List<ProductsModel>();
            IEnumerable<ProductsModel> items = null;
            HttpResponseMessage response = await hc.GetAsync("api/products");
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                //Products = JsonConvert.DeserializeObject<List<ProductsModel>>(products);
                items = JsonConvert.DeserializeObject<List<ProductsModel>>(products);
            }

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilter2"] = searchInt;

            //items = from s in Products
            //        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString));

            }

            if (searchInt != null)
            {
                items = items.Where(s => s.Category.Equals(searchInt));

            }


            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Category":
                    items = items.OrderBy(s => s.Category);
                    break;
                case "category_desc":
                    items = items.OrderByDescending(s => s.Category);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;


                default:
                    items = items.OrderBy(s => s.Id);
                    break;
            }

            List<ProductsModel> Products = new List<ProductsModel>(items);
            return View(Products);

        }

        // GET: ProductsAndServices/Details/5
        public async Task<IActionResult> GetProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetHC();
            HttpResponseMessage response = await hc.GetAsync("api/products/" + id.ToString());

            var product = response.Content.ReadAsStringAsync().Result;

            if (product == null)
            {
                return NotFound();
            }

            var item = JsonConvert.DeserializeObject<ProductsModel>(product);
            
            return View(item);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetHC();
            HttpResponseMessage response = await hc.GetAsync("api/products/" + id.ToString());

            var product = response.Content.ReadAsStringAsync().Result;

            if (product == null)
            {
                return NotFound();
            }

            var item = JsonConvert.DeserializeObject<ProductsModel>(product);

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Category")] ProductsModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //var jsonString = JsonConvert.SerializeObject(product);

                //HttpContent httpContent = new StringContent(jsonString);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                SetHC();
                HttpResponseMessage response = await hc.PutAsJsonAsync("api/products/" + id.ToString(), product);
                                
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Price,Category")] ProductsModel product)
        {
            if (ModelState.IsValid)
            {
                //var jsonString = JsonConvert.SerializeObject(product);

                //HttpContent httpContent = new StringContent(jsonString);
                //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                SetHC();
                HttpResponseMessage response = await hc.PostAsJsonAsync("api/products", product);

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SetHC();
            HttpResponseMessage response = await hc.GetAsync("api/products/" + id.ToString());

            var product = response.Content.ReadAsStringAsync().Result;

            if (product == null)
            {
                return NotFound();
            }

            var item = JsonConvert.DeserializeObject<ProductsModel>(product);

            return View(item);
        }

        // POST: ProductsAndServices/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            SetHC();
            HttpResponseMessage response = await hc.DeleteAsync("api/products/" + id.ToString());

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}