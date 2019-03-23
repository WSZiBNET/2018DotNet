using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Contact ContactDetails { get; set; }
        public Address UserAddres { get; set; }

        protected User(int id, string name, string surname, Contact contact, Address address)
        {
            Id = id;
            Name = name;
            Surname = surname;
            ContactDetails = contact;
            UserAddres = address;

        }

        protected User(int id, string name, string surname, Contact contact)
        {
            Id = id;
            Name = name;
            Surname = surname;
            ContactDetails = contact;
        }

        protected User()
        {

        }
    }
}
