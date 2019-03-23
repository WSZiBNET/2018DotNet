using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public Contact(string phoneNumber, string emailAddress)
        {
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
        }

        public Contact(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
