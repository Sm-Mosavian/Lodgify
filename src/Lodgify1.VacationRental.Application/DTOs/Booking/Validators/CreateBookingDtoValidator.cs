using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Booking.Validators
{
    public class CreateBookingDtoValidator : AbstractValidator<CreateBookingDto>
    {

        public CreateBookingDtoValidator(Domain.Rental rental)
        {
            Include(new IBookingDtoValidator(rental));
        }
    }
}
