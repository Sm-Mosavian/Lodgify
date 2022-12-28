using System.Net.Http.Json;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Application.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Lodgify1.VacationRental.Api.FunctionalTests.Controllers
{
    public class BookingControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public BookingControllerTests(WebApplicationFactory<Program> factory) 
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAGetReturnsTheCreatedBooking()
        {
            var postRentalRequest = new CreateRentalDto()
            {
                Units = 4,
                PreparationTimeInDays = 2
            };

            BaseCommandResponse postRentalResult;
            using (var postRentalResponse = await _client.PostAsJsonAsync($"/api/v1/rentals", postRentalRequest))
            {
                Assert.True(postRentalResponse.IsSuccessStatusCode);
                postRentalResult = await postRentalResponse.Content.ReadFromJsonAsync<BaseCommandResponse>();
            }

            var postBookingRequest = new CreateBookingDto()
            {
                RentalId = postRentalResult.Id,
                Nights = 3,
                Start = new DateTime(2001, 01, 01),
            };

            BaseCommandResponse postBookingResult;
            using (var postBookingResponse = await _client.PostAsJsonAsync($"/api/v1/bookings", postBookingRequest))
            {
                Assert.True(postBookingResponse.IsSuccessStatusCode);
                postBookingResult = await postBookingResponse.Content.ReadFromJsonAsync<BaseCommandResponse>();
            }

            using (var getBookingResponse = await _client.GetAsync($"/api/v1/bookings/{postBookingResult.Id}"))
            {
                Assert.True(getBookingResponse.IsSuccessStatusCode);

                var getBookingResult = await getBookingResponse.Content.ReadFromJsonAsync<BookingDto>();
                Assert.Equal(postBookingRequest.RentalId, getBookingResult.RentalId);
                Assert.Equal(postBookingRequest.Nights, getBookingResult.Nights);
                Assert.Equal(postBookingRequest.Start, getBookingResult.Start);
                Assert.Equal(postRentalRequest.PreparationTimeInDays, getBookingResult.PreparationTimeInDays);
            }
        }

        [Fact]
        public async Task GivenCompleteRequest_WhenPostBooking_ThenAPostReturnsErrorWhenThereIsOverbooking()
        {
            var postRentalRequest = new CreateRentalDto()
            {
                Units = 1,
                PreparationTimeInDays = 3
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
                Nights = 3,
                Start = new DateTime(2002, 01, 01)
            };

            using (var postBooking1Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking1Request))
            {
                Assert.True(postBooking1Response.IsSuccessStatusCode);
            }

            var postBooking2Request = new CreateBookingDto()
            {
                RentalId = postRentalResult.Id,
                Nights = 1,
                Start = new DateTime(2002, 01, 02)
            };

            BaseCommandResponse postBooking2Result;
            using (var postBooking2Response = await _client.PostAsJsonAsync($"/api/v1/bookings", postBooking2Request))
            {
                Assert.True(postBooking2Response.IsSuccessStatusCode);
                postBooking2Result = await postBooking2Response.Content.ReadFromJsonAsync<BaseCommandResponse>();
                Assert.False(postBooking2Result.Success);
                Assert.Equal(0, postBooking2Result.Id);
                Assert.Equal("Creation Failed", postBooking2Result.Message);
                Assert.Contains("Not available rental", postBooking2Result.Errors);

            }

        }
    }
}
