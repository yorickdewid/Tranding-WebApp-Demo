namespace Trade.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Trade.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Trade.DBCreate>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Trade.DBCreate";
        }

        protected override void Seed(Trade.DBCreate context)
        {
            //  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(new User { Name = "Arie" });
            context.SaveChanges();
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            Int32 unixTimestamp2 = (Int32)(DateTime.UtcNow.AddHours(3).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            context.Orders.AddOrUpdate(new Order { Amount = 500, Currency = "AUS", BuyDate = unixTimestamp, SellDate = unixTimestamp2, UserId = context.Users.First().Id });
            context.Wallets.AddOrUpdate(new Wallet { Amount = 1000, UserId = context.Users.First() });
            context.SaveChanges();
        }
    }
}
