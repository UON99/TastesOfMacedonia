using Microsoft.AspNetCore.Mvc;
using Reservations.Data;
using Reservations.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reservations.Controllers
{
    [Route("api/[Controller]")]
    public class ReservationsController : System.Web.Http.ApiController
    {
        private readonly Context _context;

        public ReservationsController(Context context)
        {
            this._context = context;
        }

        [HttpGet]
        // GET: Reservations
        public List<Reservation> Get()
        {
            return this._context.Reservations.ToList();
        }
        [HttpPost]
        public ActionResult PostNewReservation([FromBody] Reservation res)
        {

            Console.WriteLine(res);
            _context.Reservations.Add(res);
            _context.SaveChanges();

            return new OkResult();
        }

    }
}
