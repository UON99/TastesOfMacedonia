using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Models
{
    public class Reservation
    {
            public int Id { get; set; }
            public string user { get; set; }
            public Nullable<System.DateTime> datetime { get; set; }
            public string restaurant_name { get; set; }
        
    }
}
