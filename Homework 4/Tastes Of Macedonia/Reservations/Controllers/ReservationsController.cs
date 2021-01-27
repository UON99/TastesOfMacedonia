using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.Data;
using Reservations.Models;

namespace Reservations.Controllers
{
    [Route("api/[Controller]")]
    public class ReservationsController : System.Web.Http.ApiController
    {
        private Context _context;

        public ReservationsController(Context _context)
        {
            this._context = _context;
        }

        [HttpGet]
        // GET: Reservations
        public List<Reservation> Get()
        {
            return this._context.reservations.ToList();
        }
        [HttpPost]
        public ActionResult PostNewReservation([FromBody] Reservation res)
        {

            Console.WriteLine(res);
            _context.reservations.Add(res);
            _context.SaveChanges();

            return new OkResult();
        }

    }
}
