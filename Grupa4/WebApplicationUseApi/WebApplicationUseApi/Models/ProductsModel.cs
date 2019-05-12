using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationUseApi.Models
{
    public partial class ProductsModel
    {
        public int Id { get; set; }
        [Required]
        [ForeignKey("DictCategory")]
        [Display(Name = "Kategoria")]
        public int Category { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Cena")]
        public double Price { get; set; }
        //public DictCategory CategoryDict { get; set; }
    }

    public enum DictCategory { Service, Product }
}
