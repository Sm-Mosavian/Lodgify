using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Calendar.Validators
{
    public class GetCalendarDtoValidator : AbstractValidator<GetCalendarDto>
    {
        public GetCalendarDtoValidator()
        {
                RuleFor(p => p.nights)
               .GreaterThan(0)
               .WithMessage("Nights must be positive.");
        }
    }
}
