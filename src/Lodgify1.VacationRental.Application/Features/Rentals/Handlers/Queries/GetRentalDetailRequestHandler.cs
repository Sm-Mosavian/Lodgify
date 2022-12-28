using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Queries;
using Lodgify1.VacationRental.Domain;
using MediatR;

namespace Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Queries
{
    public class GetRentalDetailRequestHandler : IRequestHandler<GetRentalDetailRequest, RentalDto>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMapper _mapper;

        public GetRentalDetailRequestHandler(IRentalRepository rentalRepository, IMapper mapper)
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
        }
        public async Task<RentalDto> Handle(GetRentalDetailRequest request, CancellationToken cancellationToken)
        {
            var rental = await _rentalRepository.Get(request.Id);

            if (rental == null)
                throw new NotFoundException(nameof(Rental), request.Id);

            var mappedRental = _mapper.Map<RentalDto>(rental);

            return mappedRental;
        }
    }
}
