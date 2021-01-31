using System;

namespace Reservations.Models
{
    public class Reservation
    {
        public int id { get; set; }
        public string user { get; set; }
        public Nullable<System.DateTime> datetime { get; set; }
        public string restaurant_name { get; set; }

    }
}
