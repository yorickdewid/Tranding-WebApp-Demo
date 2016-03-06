using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            Wallet wallet = db.Wallets.Find(id);
            if (wallet == null)
            {
                return NotFound();
            }
            wallet.Trades = db.Orders.Where(x => x.UserId.Id == wallet.UserId.Id).ToList();

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
