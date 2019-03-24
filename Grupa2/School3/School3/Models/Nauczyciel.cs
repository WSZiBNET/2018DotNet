using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace School3.Models
{
    public class Nauczyciel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        [ForeignKey("Przedmiot")]
        public int PrzedmiotId { get; set; }


    }
}
