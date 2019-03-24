using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School3.Models
{
    public class Uczen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Rocznik { get; set; }
        public string Klasa { get; set; }




    }
}
