namespace Lodgify1.VacationRental.Application.DTOs.Calendar
{
    public  class GetCalendarDto
    {
        public int rentalId { get; set; }
        public DateTime start { get; set; }
        public int nights { get; set; }
    }
}
