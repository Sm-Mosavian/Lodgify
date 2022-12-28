namespace Lodgify1.VacationRental.Application.DTOs.Rental
{
    public class CreateRentalDto: IRentalDto
    {
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
