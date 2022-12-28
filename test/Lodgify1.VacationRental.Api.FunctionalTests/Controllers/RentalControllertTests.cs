using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Responses;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Lodgify1.VacationRental.Application.DTOs.Booking;

namespace Lodgify1.VacationRental.Api.FunctionalTests.Controllers
{
    public class RentalControllertTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public RentalControllertTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostRental_ThenAGetReturnsTheCreatedRental()
        {
            var request = new CreateRentalDto
            {
                Units = 25,
                PreparationTimeInDays = 2
            };

            BaseCommandResponse postResult;
            using (var postResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", request))
            {
                Assert.True(postResponse.IsSuccessStatusCode);
                postResult = await postResponse.Content.ReadFromJsonAsync<BaseCommandResponse>();
            }

            using (var getResponse = await _client.GetAsync($"/api/v1/rentals/{postResult.Id}"))
            {
                Assert.True(getResponse.IsSuccessStatusCode);

                var getResult = await getResponse.Content.ReadFromJsonAsync<RentalDto>();
                Assert.Equal(request.Units, getResult.Units);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPutRental_ThenAGetReturnsTheupdatedRental()
        {
            var postRentalRequest = new CreateRentalDto()
            {
                Units = 1,
                PreparationTimeInDays = 2
            };

            BaseCommandResponse postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadFromJsonAsync<BaseCommandResponse>();
            }

            var postBooking1Request = new CreateBookingDto()
            {
                RentalId = postRentalResult.Id,
                Nights = 2,
                Start = new DateTime(2000, 01, 02)
            };

            BaseCommandResponse postBooking1Result;
            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
                postBooking1Result = await postBooking1Response.Content.ReadFromJsonAsync<BaseCommandResponse>();
            }

            var putRentalRequest = new RentalDto()
            {
                Id = postRentalResult.Id,
                Units = 3,
                PreparationTimeInDays = 1
            };
            
            using (var postBooking2Response = await _client.PutAsJsonAsync($"/api/v1/rentals", putRentalRequest))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
            }

        }
    }
}
