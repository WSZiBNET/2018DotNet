using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalGroup3.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        public string Marka { get; set; }
        [Required]
        public string Typ { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime RokProdukcji { get; set; }
        [Required]
        public string Wyposazenie { get; set; }
        [Required]
        public decimal CenaZaDzien { get; set; }
        [Required]
        public decimal Spalanie { get; set; }

    }
}
