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

        // GET
        [ResponseType(typeof(Wallet))]
        public IHttpActionResult GetWallet(int id)
        {
            db.Orders.Load();
            db.Users.Load();
            Wallet wallet = null;
            IEnumerable<Wallet> tmp = db.Wallets.Where(x => x.UserId.Id == id);
            if (tmp.Count() > 0)
            {
                wallet = tmp.First();
            }
            if (wallet == null)
            {
                return NotFound();
            }
            try
            {
                wallet.Trades = db.Orders.Where(x => x.UserId == wallet.UserId.Id).ToList();
            }
            catch (Exception e)
            {
                Console.Write("test" + e.Data);
            }

            return Json(wallet);
        }

        // PUT
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
