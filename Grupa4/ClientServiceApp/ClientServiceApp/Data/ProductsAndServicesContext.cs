using ClientServiceApp.Models;
using ClientServiceApp.Models.ProductsAndServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Data
{
    public class ProductsAndServicesContext: DbContext
    {
        public DbSet<ProductsAndServices> Products { get; set; }
        public ProductsAndServicesContext(DbContextOptions<ProductsAndServicesContext> options) : base(options)
        {
        }
    }
}
