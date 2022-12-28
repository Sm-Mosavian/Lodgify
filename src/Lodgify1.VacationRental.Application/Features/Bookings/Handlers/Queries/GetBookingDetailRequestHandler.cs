using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries;
using Lodgify1.VacationRental.Domain;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Queries
{
    public class GetBookingDetailRequestHandler:IRequestHandler<GetBookingDetailRequest,BookingDto>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingDetailRequestHandler(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<BookingDto> Handle(GetBookingDetailRequest request, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetBookingWithDetails(request.Id);

            if (booking == null)
                throw new NotFoundException(nameof(Booking), request.Id);

            var mappedBooking=_mapper.Map<BookingDto>(booking);

            return mappedBooking;
        }
    }
}
