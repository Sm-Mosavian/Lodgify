using Lodgify1.VacationRental.Domain.Common;

namespace Lodgify1.VacationRental.Domain
{
    public class Rental : BaseDomainEntity
    {
        public Rental()
        {
            Bookings = new List<Booking>();
        }
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
