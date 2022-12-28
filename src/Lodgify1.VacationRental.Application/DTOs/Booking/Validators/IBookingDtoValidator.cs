using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Booking.Validators
{
    public class IBookingDtoValidator : AbstractValidator<IBookingDto>
    {
        public IBookingDtoValidator(Domain.Rental rental)
        {
            RuleFor(p => p.Nights)
                .GreaterThan(0).WithMessage("{PropertyName} must be be positive.")
                .MustAsync(async (bookingDto, id, token) =>
                {
                    return checkBookedUnitsOverBooking(rental, bookingDto);
                })
            .WithMessage("Not available rental");
        }

        private bool checkBookedUnitsOverBooking(Domain.Rental rental, IBookingDto bookingDto)
        {
            var bookingDtoEnd = bookingDto.Start.AddDays(bookingDto.Nights + rental.PreparationTimeInDays);

            var overBookingCount = rental.Bookings.Count(booking =>
             bookingDto.Start <= booking.Start.AddDays(booking.Nights + booking.PreparationTimeInDays)
             && booking.Start <= bookingDtoEnd);

            return (overBookingCount < rental.Units);
        }
    }
}
