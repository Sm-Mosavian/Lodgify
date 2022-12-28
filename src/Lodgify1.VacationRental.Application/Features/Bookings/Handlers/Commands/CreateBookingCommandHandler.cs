using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking.Validators;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Commands;
using Lodgify1.VacationRental.Application.Responses;
using Lodgify1.VacationRental.Domain;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BaseCommandResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public CreateBookingCommandHandler(IBookingRepository bookingRepository, IRentalRepository rentalRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public async Task<BaseCommandResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            var rental = await _rentalRepository.GetRentalWithDetails(request.CreateBookingDto.RentalId);
            if (rental == null)
                throw new NotFoundException(nameof(Booking), request.CreateBookingDto.RentalId);

            var validator = new CreateBookingDtoValidator(rental);
            var validationResult = await validator.ValidateAsync(request.CreateBookingDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                var booking = _mapper.Map<Booking>(request.CreateBookingDto);

                booking.PreparationTimeInDays = rental.PreparationTimeInDays;

                booking = await _bookingRepository.Add(booking);

                response.Success = true;
                response.Message = "Creation Sucessful";
                response.Id =booking.Id ;
            }
            return response;
        }
    }
}
