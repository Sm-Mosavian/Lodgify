using Lodgify1.VacationRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lodgify1.VacationRental.Presistence
{
    public class VacationRentalDbContext:DbContext
    {
        public VacationRentalDbContext(DbContextOptions<VacationRentalDbContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(VacationRentalDbContext).Assembly);
        }

        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}