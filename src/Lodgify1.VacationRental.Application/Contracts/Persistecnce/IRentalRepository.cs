using Lodgify1.VacationRental.Domain;

namespace Lodgify1.VacationRental.Application.Contracts.Persistecnce
{
    public interface IRentalRepository : IGenericRepository<Rental>
    {
        public Task<Rental> GetRentalWithDetails(int id);

        public Task<List<Rental>> GetAllRentalWithDetails();
    }
}
