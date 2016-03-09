using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Trade.Controllers
{
    public class ForexController : ApiController
    {
        private DBCreate db = new DBCreate();

        //GET api/order/
        public IHttpActionResult GetRates()
        {
            db.Forexes.Load();

            var collection = db.Forexes.ToList();

            dynamic collectionWrapper = new
            {
                Rates = collection
            };

            return Json(collectionWrapper);
        }

    }
}
