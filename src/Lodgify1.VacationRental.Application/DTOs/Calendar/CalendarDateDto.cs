namespace Lodgify1.VacationRental.Application.DTOs.Calendar
{
    public class CalendarDateDto
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingDto> Bookings { get; set; }
        public List<CalendarBookingDto> PreparationTimes { get; set; }
    }
}
