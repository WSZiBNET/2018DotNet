using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class Client : User
    {
        public double Discount { get; set; } = 0;
        public int ClientID { get; set; }
    }
}
