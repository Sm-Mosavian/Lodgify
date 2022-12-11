using Lodgify1.VacationRental.Application.DTOs.Calendar;
using Lodgify1.VacationRental.Application.Features.Calendars.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lodgify1.VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CalendarController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<CalendarDto>> Get(int rentalId, DateTime start, int nights)
        {
            var GetCalendarDto = new GetCalendarDto() { rentalId = rentalId, start = start, nights = nights };
            var rental = await _mediator.Send(new GetCalendarDetailRequest() { GetCalendarDto = GetCalendarDto });
            return Ok(rental);
        }


    }
}
