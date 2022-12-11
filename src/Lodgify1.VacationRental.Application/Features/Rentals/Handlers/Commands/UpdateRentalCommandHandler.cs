using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental.Validators;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands;
using Lodgify1.VacationRental.Domain;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Commands
{
    public class UpdateRentalCommandHandler : IRequestHandler<UpdateRentalCommand, Unit>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public UpdateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateRentalCommand request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.GetRentalWithDetails(request.RentalDto.Id);

            if (rental == null)
                throw new NotFoundException(nameof(Rental), request.RentalDto.Id);

            var validator = new UpdateRentalDtoValidator(rental);
            var validationResult = await validator.ValidateAsync(request.RentalDto);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            _mapper.Map(request.RentalDto, rental);

            rental.Bookings.ForEach(f => f.PreparationTimeInDays = request.RentalDto.PreparationTimeInDays);

            await _rentalRepository.Update(rental);

            return Unit.Value;
        }
    }
}
