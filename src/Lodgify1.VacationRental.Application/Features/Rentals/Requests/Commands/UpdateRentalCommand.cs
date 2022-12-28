using Lodgify1.VacationRental.Application.DTOs.Rental;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands
{
    public class UpdateRentalCommand:IRequest<Unit>
    {
        public RentalDto RentalDto { get; set; }
    }
}
