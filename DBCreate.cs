using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using trading_backend.Model;

namespace trading_backend
{
    public class DBCreate : DbContext
    {
        static void Main(string[] args)
        {
            new DBCreate();
            Console.WriteLine("Test");
        }

        public DBCreate()
        {
            Users.Add(new User { Id = 1, Name = "Arie" });
            SaveChanges();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Forex> Forexes { get; set; }
    }
}