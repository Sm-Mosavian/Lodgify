using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Moq;

namespace Lodgify1.VacationRental.UnitTests.Mocks
{
    public static class MockBookingRepository
    {
        public static Mock<IBookingRepository> GetBookingRepository()
        {
            var bookings = new List<Domain.Booking>
            {
               new Domain.Booking
                        {
                            Id=1,
                            Nights=3,
                            PreparationTimeInDays=2,
                            Start=new DateTime(2022,12,10),
                            RentalId=1,
                        },
               new Domain.Booking
                        {
                            Id=2,
                            Nights=2,
                            PreparationTimeInDays=2,
                            Start=new DateTime(2022,12,10),
                            RentalId=1,
                        },
               new Domain.Booking
                        {
                            Id=3,
                            Nights=2,
                            PreparationTimeInDays=2,
                            Start=new DateTime(2022,12,11),
                            RentalId=1,
                        }

            };

            var mockRepo = new Mock<IBookingRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(bookings);

            mockRepo.Setup(r => r.GetBookingWithDetails()).ReturnsAsync(bookings);

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Booking>())).ReturnsAsync((Domain.Booking booking) =>
            {
                bookings.Add(booking);
                return booking;
            });

            mockRepo.Setup(r => r.GetBookingWithDetails(1)).ReturnsAsync(bookings.FirstOrDefault(w => w.Id == 1));

            return mockRepo;
        }
    }
}
