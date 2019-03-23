using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HomeNumber { get; set; }

        public Address(string city, string street, string homeNumber)
        {
            City = city;
            Street = street;
            HomeNumber = homeNumber;
        }
    }
}
