using ClientServiceApp.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace ClientServiceApp.Models
{
    public class Product: IServiceCategory
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Category { get; set; }


    }
}
