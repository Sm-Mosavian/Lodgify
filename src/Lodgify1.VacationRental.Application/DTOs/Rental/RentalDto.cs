using Lodgify1.VacationRental.Application.DTOs.Common;

namespace Lodgify1.VacationRental.Application.DTOs.Rental
{
    public class RentalDto:BaseDto, IRentalDto
    {
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
