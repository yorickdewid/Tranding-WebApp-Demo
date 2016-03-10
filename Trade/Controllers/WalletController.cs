using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Trade.Models;

namespace Trade.Controllers
{
    public class WalletController : ApiController
    {
        private DBCreate db = new DBCreate();

        [ResponseType(typeof(Wallet))]
        public IHttpActionResult GetWallet(int id)
        {
            db.Orders.Load();
            db.Users.Load();
            Wallet wallet = null;
            var tmp = from w in db.Wallets
                         where w.UserId.Id == id
                         select w;
            if (tmp == null)
            {
                return NotFound();
            }
            try
            {
                wallet = tmp.First();
                wallet.Trades = db.Orders.Where(x => x.UserId == wallet.UserId.Id).ToList();
            }
            catch (Exception e)
            {
                Console.Write("test" + e.Data);
            }

            return Json(wallet);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutWallet(int id, Wallet wallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != wallet.Id)
            {
                return BadRequest();
            }

            db.Entry(wallet).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
