using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trading_backend.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool OrderType { get; set; }
    }
}