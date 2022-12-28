using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Calendar;
using Lodgify1.VacationRental.Application.Features.Calendars.Handlers.Queries;
using Lodgify1.VacationRental.Application.Features.Calendars.Requests.Queries;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Calendar.Queries
{

    public class GetCalendarDetailRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRentalRepository> _mockRentalRepo;
        private readonly GetCalendarDetailRequestHandler _handler;
        public GetCalendarDetailRequestHandlerTests()
        {
            _mockRentalRepo = MockRentalRepository.GetRentalRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetCalendarDetailRequestHandler(_mockRentalRepo.Object);
        }
        [Fact]
        public async void WhenGetCalendar_ThenReturnListOfDateOfRentalWithChildListGroupingDetails()
        {
            var getCalendarDto = new GetCalendarDto()
            {
                rentalId = 1,
                start = new DateTime(2022, 12, 9),
                nights = 8
            };

            var result = await _handler.Handle(new GetCalendarDetailRequest() { GetCalendarDto = getCalendarDto }, CancellationToken.None);

            result.ShouldBeOfType<CalendarDto>();

            result.Dates.Count.ShouldBe(getCalendarDto.nights);

           

            //result.Id.ShouldBe(1);

        }
    }
}
