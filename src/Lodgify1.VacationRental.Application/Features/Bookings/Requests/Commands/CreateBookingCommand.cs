using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Responses;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Bookings.Requests.Commands
{
    public class CreateBookingCommand:IRequest<BaseCommandResponse>
    {
        public CreateBookingDto CreateBookingDto { get; set; }
    }
}
