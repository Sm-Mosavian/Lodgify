using Lodgify1.VacationRental.Application.DTOs.Calendar;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Calendars.Requests.Queries
{
    public class GetCalendarDetailRequest : IRequest<CalendarDto>
    {
       public GetCalendarDto GetCalendarDto { get; set; }
      
    }
}
