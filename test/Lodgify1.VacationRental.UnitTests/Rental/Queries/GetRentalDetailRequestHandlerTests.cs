using AutoMapper;
using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Exceptions;
using Lodgify1.VacationRental.Application.Features.Rentals.Handlers.Queries;
using Lodgify1.VacationRental.Application.Features.Rentals.Requests.Queries;
using Lodgify1.VacationRental.Application.Profiles;
using Lodgify1.VacationRental.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace Lodgify1.VacationRental.UnitTests.Rental.Queries
{
    public class GetRentalDetailRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRentalRepository> _mockRepo;
        private readonly GetRentalDetailRequestHandler _handler;
        public GetRentalDetailRequestHandlerTests()
        {
            _mockRepo = MockRentalRepository.GetRentalRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new GetRentalDetailRequestHandler(_mockRepo.Object, _mapper);
        }

        [Fact]
        public async void WhenGetRentalwithDetail_ThenReturnCorrectRentalwithItsDetail()
        {
            var result = await _handler.Handle(new GetRentalDetailRequest() { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<RentalDto>();

            result.Id.ShouldBe(1);
        }

        [Fact]
        public async Task WhenGetNotExistRental_ThenReturnProperException()
        {
            NotFoundException ex = await Should.ThrowAsync<NotFoundException>
            (async () =>
                 await _handler.Handle(new GetRentalDetailRequest() { Id = -1 }, CancellationToken.None)
            );
        }
    }
}
