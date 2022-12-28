using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lodgify1.VacationRental.Presistence.Repositories
{
    public class RentalRepository : GenericRepository<Rental>, IRentalRepository
    {
        private readonly VacationRentalDbContext _dbContext;

        public RentalRepository(VacationRentalDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Rental> GetRentalWithDetails(int id)
        {
            var rental = await _dbContext.Rentals
                 .Include(q => q.Bookings)
                  .FirstOrDefaultAsync(q => q.Id == id);

            return rental;

        }

        public async Task<List<Rental>> GetAllRentalWithDetails()
        {
            var rental = await _dbContext.Rentals
                 .Include(q => q.Bookings)
                  .ToListAsync();

            return rental;

        }
    }
}
