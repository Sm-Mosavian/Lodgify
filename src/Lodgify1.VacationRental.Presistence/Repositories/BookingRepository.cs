using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lodgify1.VacationRental.Presistence.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly VacationRentalDbContext _dbContext;

        public BookingRepository(VacationRentalDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking> GetBookingWithDetails(int id)
        {
            var bookingRequest = await _dbContext.Bookings
                .Include(q => q.Rental)
                .FirstOrDefaultAsync(q => q.Id == id);

            return bookingRequest;

        }

        public async Task<List<Booking>> GetBookingWithDetails()
        {
            var bookingRequests = await _dbContext.Bookings
                .Include(q => q.Rental)
                .ToListAsync();

            return bookingRequests;
        }

       
    }
}
