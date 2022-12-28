using Lodgify1.VacationRental.Application.DTOs.Booking;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries
{
    public class GetBookingListRequest:IRequest<List<BookingDto>>
    {
    }
}
