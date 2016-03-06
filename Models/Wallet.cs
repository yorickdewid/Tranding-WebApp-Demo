using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace trading_backend.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public double Amount { get; set; }
        public List<Order> Orders { get; set; }

        public bool removeAmount(double Amount)
        {
            if( ! ((this.Amount - Amount) < 0) )
            {
                return true;
            }
            return false;
        }

        public double addAmount(double Amount)
        {
            this.Amount += Amount;
            return this.Amount;
        }
    }
}