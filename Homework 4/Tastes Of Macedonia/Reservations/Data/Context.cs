using Microsoft.EntityFrameworkCore;
using Reservations.Models;

namespace Reservations.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Reservation> Reservations { get; set; }


    }
}
