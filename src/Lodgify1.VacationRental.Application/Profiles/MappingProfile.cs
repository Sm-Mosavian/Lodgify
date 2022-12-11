using AutoMapper;
using Lodgify1.VacationRental.Application.DTOs.Booking;
using Lodgify1.VacationRental.Application.DTOs.Rental;
using Lodgify1.VacationRental.Domain;

namespace Lodgify1.VacationRental.Application.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            #region Rental Mapping

            CreateMap<Rental, RentalDto>().ReverseMap();
            CreateMap<Rental, CreateRentalDto>().ReverseMap();

            #endregion Rental

            #region Booking Mapping

            CreateMap<Booking, BookingDto>().ReverseMap();
            CreateMap<Booking, CreateBookingDto>().ReverseMap();

            #endregion Booking
        }
    }
}
