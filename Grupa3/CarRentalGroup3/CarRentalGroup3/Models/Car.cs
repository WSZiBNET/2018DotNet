using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalGroup3.Models
{
    public class Car
    {
        [Required]
        public string marka { get; set; }
        [Required]
        public string typ { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime rokProdukcji { get; set; }
        [Required]
        public string wyposazenie { get; set; }
        [Required]
        public decimal cenaZaDzien { get; set; }
        [Required]
        public decimal spalanie { get; set; }

    }
}
