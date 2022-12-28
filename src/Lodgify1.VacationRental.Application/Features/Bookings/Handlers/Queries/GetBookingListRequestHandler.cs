using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Queries
{
    public class GetBookingListRequestHandler : IRequestHandler<GetBookingListRequest, List<BookingDto>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingListRequestHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<List<BookingDto>> Handle(GetBookingListRequest request, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingWithDetails();
            var mappedBookings=_mapper.Map<List<BookingDto>>(bookings);
            return mappedBookings;
        }
    }
}
