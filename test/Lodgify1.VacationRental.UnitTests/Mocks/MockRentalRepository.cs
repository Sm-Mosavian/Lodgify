using Lodgify1.VacationRental.Application.Contracts.Persistecnce;
using Moq;

namespace Lodgify1.VacationRental.UnitTests.Mocks
{
    public static class MockRentalRepository
    {
        public static Mock<IRentalRepository> GetRentalRepository()
        {
            var rentals = new List<Domain.Rental>
            {
                new Domain.Rental
                {
                    Id=1,
                    Units=3,
                    PreparationTimeInDays=2,
                    Bookings=new List<Domain.Booking>
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
                            Start=new DateTime(2022,12,11),
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
                    }
                },
                new Domain.Rental
                {
                    Id=2,
                    Units=4,
                    PreparationTimeInDays=2,
                   
                }
            };

            var mockRepo = new Mock<IRentalRepository>();

            mockRepo.Setup(r => r.GetAll()).ReturnsAsync(rentals);

            mockRepo.Setup(r => r.Get(1)).ReturnsAsync(rentals.FirstOrDefault(f => f.Id == 1));

            mockRepo.Setup(r => r.Add(It.IsAny<Domain.Rental>())).ReturnsAsync((Domain.Rental rental) =>
            {
                rentals.Add(rental);
                return rental;
            });

            mockRepo.Setup(r => r.GetRentalWithDetails(1)).ReturnsAsync(rentals.FirstOrDefault(w => w.Id == 1));

            mockRepo.Setup(r => r.GetRentalWithDetails(2)).ReturnsAsync(rentals.FirstOrDefault(w => w.Id == 2));

            // mockRepo.Setup(r => r.GetAllRentalWithDetails()).ReturnsAsync(rentals);

            return mockRepo;
        }
    }
}
