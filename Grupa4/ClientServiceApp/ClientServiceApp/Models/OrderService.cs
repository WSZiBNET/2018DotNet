using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class OrderService
    {

        public int Id { get; set; }
        public Service Service { get; set; }
        public int ServiceId { get; set; }
        public DateTime SaleDate { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Employee Seller { get; set; }
        public int SellerId { get; set; }
        public int Amount { get; set; }
    }
}
