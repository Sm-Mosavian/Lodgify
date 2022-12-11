using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Rental.Validators
{
    public class CreateRentalDtoValidator:AbstractValidator<CreateRentalDto>
    {
        public CreateRentalDtoValidator()
        {
            Include(new IRentalDtoValidator());
        }
    }
}
