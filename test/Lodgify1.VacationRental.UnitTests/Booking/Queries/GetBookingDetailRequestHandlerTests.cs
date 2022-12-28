using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Bookings.Handlers.Queries;
using Lodgify1.VacationRental.Application.Features.Bookings.Requests.Queries;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Booking.Queries
{
    public class GetBookingDetailRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBookingRepository> _mockRepo;
        private readonly GetBookingDetailRequestHandler _handler;
 
        public GetBookingDetailRequestHandlerTests()
        {
            _mockRepo = MockBookingRepository.GetBookingRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetBookingDetailRequestHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async void WhenGetValidBooking_ThenBookingwithDetailsReturned()
        {
            var result = await _handler.Handle(new GetBookingDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<BookingDto>();

            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task WhenGetBookingWithInvalidRentalId_ThenNotExistExceptionReturned()
        {
            NotFoundException ex = await Should.ThrowAsync<NotFoundException>
            (async () =>
                 await _handler.Handle(new GetBookingDetailRequest() { Id = -1 }, CancellationToken.None)
            );
        }
    }
}
