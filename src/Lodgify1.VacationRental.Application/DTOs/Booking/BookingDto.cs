using Lodgify1.VacationRental.Application.DTOs.Common;

namespace Lodgify1.VacationRental.Application.DTOs.Booking
{
    public class BookingDto:BaseDto, IBookingDto
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
