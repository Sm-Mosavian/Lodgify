using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Queries;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Booking.Queries
{
    public class GetBookingListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBookingRepository> _mockRepo;
        private readonly GetBookingListRequestHandler _handler;
     
        public GetBookingListRequestHandlerTests()
        {
            _mockRepo = MockBookingRepository.GetBookingRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetBookingListRequestHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async void WhenGetBookingWithRentalId_ThenListOfBookingReturned()
        {
            var result = await _handler.Handle(new GetBookingListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<BookingDto>>();

            result.Count.ShouldBe(3);
        }
    }
}
