using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Trade.Models;

namespace Trade
{
    public class DBCreate : DbContext
    {

        public DBCreate()
        {
            Users.Add(new User { Name = "Arie" });
            SaveChanges();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.AddHours(3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Orders.Add(new Order { Amount = 500, Currency = "AUS", BuyDate = unixTimestamp, SellDate = unixTimestamp2, OrderType = true, UserId = Users.First() });
            SaveChanges();
            Wallets.Add(new Wallet { Amount = 1000, UserId = Users.First(), Trades = Orders.ToList()});
            SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Forex> Forexes { get; set; }
    }
}