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
    public class UserController : ApiController
    {

        private DBCreate db = new DBCreate();

        public IHttpActionResult GetAllUsers()
        {
            db.Users.Load();

            var collection = db.Users.ToList();

            dynamic collectionWrapper = new
            {
                Users = collection
            };

            return Json(collectionWrapper);
        }

        [ResponseType(typeof(User))]
        public IHttpActionResult GetUser(int id)
        {
            db.Users.Load();
            User user = db.Users.Find(id);
            if (User == null)
            {
                return NotFound();
            }

            return Json(User);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

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

        [ResponseType(typeof(User))]
        public IHttpActionResult DeleteUser(int id)
        {
            User user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            db.SaveChanges();

            return Ok(user);
        }
    }
}
