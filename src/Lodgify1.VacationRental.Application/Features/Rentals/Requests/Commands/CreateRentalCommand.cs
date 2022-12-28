using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Responses;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands
{
    public class CreateRentalCommand : IRequest<BaseCommandResponse>
    {
        public CreateRentalDto CreateRentalDto { get; set; }
    }
}
