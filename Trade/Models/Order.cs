﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trade.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Currency { get; set; }
        public long BuyDate { get; set; }
        public long SellDate { get; set; }
        public double BuyRate { get; set; }
        public double SellRate { get; set; }
        public int Amount { get; set; }
    }
}