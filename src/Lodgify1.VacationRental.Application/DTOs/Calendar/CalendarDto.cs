namespace Lodgify1.VacationRental.Application.DTOs.Calendar
{
    public class CalendarDto
    {
        public int RentalId { get; set; }
        public List<CalendarDateDto> Dates { get; set; }
    }
}
