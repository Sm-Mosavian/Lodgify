using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental.Validators;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands;
using Lodgify1.VacationRental.Application.Responses;
using Lodgify1.VacationRental.Domain;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Commands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, BaseCommandResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var validator = new CreateRentalDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateRentalDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                var rental = _mapper.Map<Rental>(request.CreateRentalDto);

                rental = await _rentalRepository.Add(rental);
                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = rental.Id;
            }
            return response;
        }
    }
}
