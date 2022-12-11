using System.ComponentModel.DataAnnotations.Schema;
using Lodgify1.VacationRental.Domain.Common;


namespace Lodgify1.VacationRental.Domain
{
    public class Booking:BaseDomainEntity
    {
        [ForeignKey("RentalId")]
        public Rental Rental { get; set; }

        public int RentalId { get; set; }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;
        public int Nights { get;  set; }
       
        public int PreparationTimeInDays { get; set; }
             

    }
}
