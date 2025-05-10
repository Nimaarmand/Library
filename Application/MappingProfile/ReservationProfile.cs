using Application.Dtos.Books;
using AutoMapper;
using Domain.Entities.Reservations;

namespace Application.MappingProfile
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();

        }

    }
}
