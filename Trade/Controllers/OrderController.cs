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

        //GET api/order/
        public IHttpActionResult GetAllProducts()
        {
            return Json(db.Orders);
        }

        //GET api/order/3
        public IHttpActionResult GetOrderById(int id)
        {
            Order o = db.Orders.Find(id);
            if (o == null)
                return NotFound();
            return Ok(o);
        }

        //insert
        // POST api/order
        public IHttpActionResult PostOrder(Order o)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            db.Orders.Add(o);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = o.Id }, o);
        }
    }
}
