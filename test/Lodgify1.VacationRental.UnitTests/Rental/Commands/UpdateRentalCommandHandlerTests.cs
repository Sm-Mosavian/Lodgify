using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Commands;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Rental.Commands
{
    public class UpdateRentalCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRentalRepository> _mockRepo;
        private readonly RentalDto _rentalDto;
        private readonly UpdateRentalCommandHandler _handler;
        public UpdateRentalCommandHandlerTests()
        {
            _mockRepo = MockRentalRepository.GetRentalRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateRentalCommandHandler(_mockRepo.Object, _mapper);

            _rentalDto = new RentalDto
            {
                Id = 1,
                Units = 4,
                PreparationTimeInDays = 1
            };
        }
        [Fact]
        //GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        public async Task WhenUpdateRentalIsValid_ThenExistRentalUpdated()
        {
            var result = await _handler.Handle(new UpdateRentalCommand() { RentalDto = _rentalDto }, CancellationToken.None);

            var rental = await _mockRepo.Object.Get(1);

            rental.Units.ShouldBe(4);

            rental.PreparationTimeInDays.ShouldBe(1);
        }

        [Fact]
        public async Task WhenUpdateRentalIsInValidValid_ThenExistRentalNotUpdatedAndReturnErrors()
        {
            _rentalDto.Units = 2;
            _rentalDto.PreparationTimeInDays = 3;


            ValidationException ex = await Should.ThrowAsync<ValidationException>
            (async () =>
               await _handler.Handle(new UpdateRentalCommand() { RentalDto = _rentalDto }, CancellationToken.None)
            );

            ex.Errors.ShouldContain("Units cannot be less than count of rental booking");

            ex.Errors.ShouldContain("Preparation Time In Days value is not valid");

            var rental = await _mockRepo.Object.Get(1);

            rental.Units.ShouldBe(3);

            rental.PreparationTimeInDays.ShouldBe(2);
        }

        [Fact]
        public async Task WhenUpdateRentalIsValid_ThenAllPreparationTimeOfExistRentalUpdated()
        {

            var result = await _handler.Handle(new UpdateRentalCommand() { RentalDto = _rentalDto }, CancellationToken.None);

            var rental = await _mockRepo.Object.GetRentalWithDetails(_rentalDto.Id);

            rental.Bookings.ShouldAllBe(p => p.PreparationTimeInDays == _rentalDto.PreparationTimeInDays);
        }
    }
}
