namespace Lodgify1.VacationRental.Application.DTOs.Booking
{
    public interface IBookingDto
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
        
    }
}
