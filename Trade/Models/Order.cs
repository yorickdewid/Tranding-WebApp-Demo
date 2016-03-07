using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trade.Models
{
    public class Order
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public string Currency { get; set; }
        public long BuyDate { get; set; }
        public long SellDate { get; set; }
        public int Amount { get; set; }
    }
}