using System.ComponentModel.DataAnnotations;

namespace School3.Models
{
    public class Uczen
    {
        [Required]
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Rocznik { get; set; }
        public string Klasa { get; set; }




    }
}
