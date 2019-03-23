using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Models
{
    public class Movies
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Date)] // oznacza że ta właściwość potrzebuje tylko date, bez godizny min itp. użytkownik nie musi wpisaywać
        public DateTime ProductionDate { get; set; }
        public float Price { get; set; }
        public string MovieDescription { get; set; }


    }
}
