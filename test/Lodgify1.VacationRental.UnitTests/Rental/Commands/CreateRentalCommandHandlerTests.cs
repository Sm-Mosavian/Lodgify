using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Commands;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Commands;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.Application.Responses;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Rental.Commands
{
    public class CreateRentalCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRentalRepository> _mockRepo;
        private readonly CreateRentalDto _createRentalDto;
        private readonly CreateRentalCommandHandler _handler;
        public CreateRentalCommandHandlerTests()
        {
            _mockRepo = MockRentalRepository.GetRentalRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateRentalCommandHandler(_mockRepo.Object, _mapper);

            _createRentalDto = new CreateRentalDto
            {
                Units = 2,
                PreparationTimeInDays = 3
            };
        }
        [Fact]
        public async Task WhenAddedRentalIsValid_ThenRentalCreatedAndObjectWhichContainIdReturned()
        {
            var result = await _handler.Handle(new CreateRentalCommand() { CreateRentalDto = _createRentalDto }, CancellationToken.None);

            var rentals = await _mockRepo.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            result.Success.ShouldBeTrue();

            result.Message = "Creation Successful";

            rentals.Count.ShouldBe(3);
        }

        [Fact]
        public async Task WhenAddedRentalIsInValid_ThenRentalNotCreatedAndErrorsReturned()
        {
            _createRentalDto.Units = -1;
            _createRentalDto.PreparationTimeInDays = -1;

            var result = await _handler.Handle(new CreateRentalCommand() { CreateRentalDto = _createRentalDto }, CancellationToken.None);
            
            result.ShouldBeOfType<BaseCommandResponse>();

            result.Errors.ShouldContain("Units must be at least 1.");

            result.Errors.ShouldContain("Preparation Time In Days must be at least 0.");

            result
                .ShouldSatisfyAllConditions(
                         response => response.Success.ShouldBeFalse(),
                         response => response.Message = "Creation Failed",
                         response => response.Errors.ShouldContain("Units must be at least 1."),
                         response => response.Errors.ShouldContain("Preparation Time In Days must be at least 0.")
                  );


            var rentals = await _mockRepo.Object.GetAll();

            rentals.Count.ShouldBe(2);
        }



    }
}
