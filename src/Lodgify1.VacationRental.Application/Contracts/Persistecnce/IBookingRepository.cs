using Lodgify1.VacationRental.Domain;

namespace Lodgify1.VacationRental.Application.Contracts.Persistecnce
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<Booking> GetBookingWithDetails(int id);

        Task<List<Booking>> GetBookingWithDetails();

      
    }
}
