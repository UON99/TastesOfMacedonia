using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication7.Models
{
    public class Item
    {
        [Key]
        public String restaurant { get; set; }
        public DateTime time { get; set; }
    }
}