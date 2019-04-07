using System;
using System.Collections.Generic;

namespace FirstAPI.Models.EF
{
    public partial class Products
    {
        public int Id { get; set; }
        public int Category { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
