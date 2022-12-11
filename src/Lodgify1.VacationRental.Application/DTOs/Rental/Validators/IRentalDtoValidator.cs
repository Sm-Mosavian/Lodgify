using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Rental.Validators
{
    public class IRentalDtoValidator:AbstractValidator<IRentalDto>
    {
        public IRentalDtoValidator()
        {
            RuleFor(p => p.Units)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} must be at least {ComparisonValue}.");

            RuleFor(p => p.PreparationTimeInDays)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .GreaterThan(0).WithMessage("{PropertyName} must be at least {ComparisonValue}.");
        }
    }
}
