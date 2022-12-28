using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Commands;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Commands;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.Application.Responses;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Booking.Commands
{
    public class CreateBookingCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBookingRepository> _mockBookingRepo;
        private readonly Mock<IRentalRepository> _mockRentalRepo;
        private readonly CreateBookingDto _createBookingDto;
        private readonly CreateBookingCommandHandler _handler;
        public CreateBookingCommandHandlerTest()
        {
            _mockBookingRepo = MockBookingRepository.GetBookingRepository();
            _mockRentalRepo = MockRentalRepository.GetRentalRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new CreateBookingCommandHandler(_mockBookingRepo.Object, _mockRentalRepo.Object, _mapper);

            _createBookingDto = new CreateBookingDto
            {
                RentalId = 1,
                Nights = 3,
                Start = new DateTime(2022, 12, 14),
            };
        }
        [Fact]
        public async Task WhenAddedBookingIsValid_ThenBookingCreatedAndObjectWhichContainIdReturned()
        {
            _createBookingDto.RentalId = 2;

            var result = await _handler.Handle(new CreateBookingCommand { CreateBookingDto = _createBookingDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            result.Success.ShouldBeTrue();

            result.Message = "Creation Successful";

            var bookings = await _mockBookingRepo.Object.GetAll();

            bookings.Count.ShouldBe(4);
        }

        [Fact]
        public async Task WhenInValidBookingAdded_ThenNightsValidateAndReturnError()
        {
            _createBookingDto.Nights = 0;
            
            var result = await _handler.Handle(new CreateBookingCommand { CreateBookingDto = _createBookingDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            result
                .ShouldSatisfyAllConditions(
                         response => response.Success.ShouldBeFalse(),
                         response => response.Message = "Creation Failed",
                         response => response.Errors.ShouldContain("Nights must be be positive.")
                  );
            
            var bookings = await _mockBookingRepo.Object.GetAll();

            bookings.Count.ShouldBe(3);
        }

        [Fact]
        public async Task WhenAddedBookingIsInValid_ThenRentalOverBookingCheckAndReturnErrors()
        {
            var result = await _handler.Handle(new CreateBookingCommand { CreateBookingDto = _createBookingDto }, CancellationToken.None);

            result.ShouldBeOfType<BaseCommandResponse>();

            result
                .ShouldSatisfyAllConditions(
                         response => response.Success.ShouldBeFalse(),
                         response => response.Message = "Creation Failed",
                         response => response.Errors.ShouldContain("Not available rental")
                  );

            var bookings = await _mockBookingRepo.Object.GetAll();

            bookings.Count.ShouldBe(3);
        }



    }
}
