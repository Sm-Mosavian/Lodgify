using Lodgify1.VacationRental.Application.DTOs.Rental;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Requests.Queries
{
    public class GetRentalDetailRequest:IRequest<RentalDto>
    {
        public int Id { get; set; }
    }
}
