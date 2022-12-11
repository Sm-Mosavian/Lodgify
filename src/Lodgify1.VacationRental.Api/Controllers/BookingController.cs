using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Commands;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries;
using Lodgify1.VacationRental.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lodgify1.VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<ActionResult<BookingDto>> Get(int bookingId)
        {
            var rental = await _mediator.Send(new GetBookingDetailRequest { Id = bookingId });
            return Ok(rental);
        }

        
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBookingDto booking)
        {
            var command = new CreateBookingCommand { CreateBookingDto = booking };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
