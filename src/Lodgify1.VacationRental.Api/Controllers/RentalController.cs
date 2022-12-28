using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Queries;
using Lodgify1.VacationRental.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Lodgify1.VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<ActionResult<RentalDto>> Get(int rentalId)
        {
            var rental = await _mediator.Send(new GetRentalDetailRequest { Id = rentalId });
            return Ok(rental);
        }

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateRentalDto rental)
        {
            var command = new CreateRentalCommand { CreateRentalDto = rental };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] RentalDto rental)
        {
            var command = new UpdateRentalCommand { RentalDto = rental };
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
