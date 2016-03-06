using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trade.Models
{
    public abstract class Model
    {
        public Model(DBCreate db)
        {
            this.db = db;
        }

        protected DBCreate db { get; set; }
    }
}