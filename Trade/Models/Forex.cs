using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trade.Models
{
    public class Forex
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Ratio { get; set; }
    }
}