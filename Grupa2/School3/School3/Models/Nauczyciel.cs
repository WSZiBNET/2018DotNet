using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
