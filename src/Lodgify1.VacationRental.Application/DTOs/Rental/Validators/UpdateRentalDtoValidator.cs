using FluentValidation;

namespace Lodgify1.VacationRental.Application.DTOs.Rental.Validators
{
    public class UpdateRentalDtoValidator : AbstractValidator<RentalDto>
    {
        private readonly Domain.Rental _rental;

        public UpdateRentalDtoValidator(Domain.Rental rental)
        {
            _rental= rental;

            Include(new IRentalDtoValidator());

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present.");
            RuleFor(p => p.Units)
                .MustAsync(unitValidation)
                .WithMessage("{PropertyName} cannot be less than count of rental booking");

            RuleFor(p => p.PreparationTimeInDays)
                .MustAsync(preparationTimeValidation)
                .WithMessage("{PropertyName} value is not valid");

        }

        private async Task<bool> unitValidation(RentalDto rentalDto, int id, CancellationToken token)
        {
           var existingBookinksCount = _rental.Bookings.Count;

            return  (rentalDto.Units < existingBookinksCount) ? false : true;
        }

        private async Task<bool> preparationTimeValidation(RentalDto rentalDto, int id, CancellationToken token)
        {
            var existingBookink = _rental.Bookings .FirstOrDefault(f => f.PreparationTimeInDays < rentalDto.PreparationTimeInDays);

            return (existingBookink != null) ? false : true;
        }
    }
}
