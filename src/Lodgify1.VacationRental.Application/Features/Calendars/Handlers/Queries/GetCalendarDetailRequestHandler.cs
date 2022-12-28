using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Calendar;
using Lodgify1.VacationRental.Application.DTOs.Calendar.Validators;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Calendars.Requests.Queries;
using Lodgify1.VacationRental.Domain;
using MediatR;


namespace Lodgify1.VacationRental.Application.Features.Calendars.Handlers.Queries
{
    public class GetCalendarDetailRequestHandler : IRequestHandler<GetCalendarDetailRequest, CalendarDto>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetCalendarDetailRequestHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }
        public async Task<CalendarDto> Handle(GetCalendarDetailRequest request, CancellationToken cancellationToken)
        {
            var validator = new GetCalendarDtoValidator();
            var validationResult = await validator.ValidateAsync(request.GetCalendarDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var rental = await _rentalRepository.GetRentalWithDetails(request.GetCalendarDto.rentalId);

            if (rental == null)
                throw new NotFoundException(nameof(Rental), request.GetCalendarDto.rentalId);

            var CalendarDateDtoList = getCalendarDateDtoList(rental, request.GetCalendarDto);

            return new CalendarDto() { RentalId = rental.Id, Dates = CalendarDateDtoList };
        }

        private List<CalendarDateDto> getCalendarDateDtoList(Rental rental, GetCalendarDto dto)
        {
            var dates = Enumerable.Range(0, dto.nights).Select(i => dto.start.AddDays(i));

            return dates.Select(s => new CalendarDateDto
            {
                Date = s.Date,
                Bookings = getBookingUnits(rental.Bookings, s.Date),
                PreparationTimes = getPreparationUnits(rental.Bookings, s.Date),
            }).ToList();
        }
        private List<CalendarBookingDto> getBookingUnits(List<Booking> bookings, DateTime baseDate)
        {
            var bookingUnits = bookings.Where(booking =>
                booking.Start <= baseDate.Date && baseDate.Date < booking.Start.AddDays(booking.Nights));

            return getCalendarBookingDtoList(bookingUnits);
        }
        private List<CalendarBookingDto> getPreparationUnits(List<Booking> bookings, DateTime baseDate)
        {
            var preparationUnits = bookings.Where(booking => booking.PreparationTimeInDays > 0 &&
                booking.Start.AddDays(booking.Nights) <= baseDate.Date &&
                baseDate.Date < booking.Start.AddDays(booking.Nights + booking.PreparationTimeInDays));

            return getCalendarBookingDtoList(preparationUnits);
        }
        private List<CalendarBookingDto> getCalendarBookingDtoList(IEnumerable<Booking> bookings)
        {
            return bookings.GroupBy(g => g.Id)
                .Select(s => new CalendarBookingDto
                {
                    Id = s.Key,
                    Unit = s.Count()
                }).ToList();
        }
    }
}
