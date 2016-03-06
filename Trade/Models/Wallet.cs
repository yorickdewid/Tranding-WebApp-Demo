using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trade.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public double Amount { get; set; }
        [NotMapped]
        public List<Order> Trades { get; set; }

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