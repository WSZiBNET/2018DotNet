using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientServiceApp.Models
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
        public Employee Seller { get; set; }
        public int SellerId { get; set; }
        public int Amount { get; set; }


    }
}
