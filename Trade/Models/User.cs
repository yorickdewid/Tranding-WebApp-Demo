using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Trade.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}