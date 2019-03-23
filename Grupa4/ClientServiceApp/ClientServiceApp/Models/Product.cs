using ClientServiceApp.Interfaces;
using System.ComponentModel.DataAnnotations;


namespace ClientServiceApp.Models
{
    public class Product: IServiceCategory
    {
                
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        [Required]
        public int Category { get; set; }


    }
}
