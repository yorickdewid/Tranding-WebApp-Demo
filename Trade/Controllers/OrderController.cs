using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Trade.Models;

namespace Trade.Controllers
{
    public class OrderController : ApiController
    {
        private DBCreate db = new DBCreate();

        [HttpGet]
        public IHttpActionResult GetOrders()
        {
            return Json(db.Orders);
        }

        [HttpGet]
        public IHttpActionResult GetOrderById(int id)
        {
            Order o = db.Orders.Find(id);
            if (o == null)
                return NotFound();
            return Ok(o);
        }

        [HttpPut]
        public IHttpActionResult PutOrder(Order o)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            db.Entry(o).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(o.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        public IHttpActionResult PostOrder(Order o)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Orders.Add(o);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = o.Id }, o);
        }

        private bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }
    }
}
