using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientServiceApp.Models.ProductsAndServices
{
    public class ProductsAndServices
    {
                
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        [Required]
        [ForeignKey("DictCategory")]
        public int Category { get; set; }


    }

    enum DictCategory {Service, Product}
}
