using Application.Dtos.Books;
using AutoMapper;
using Domain.Entities.Books;
using Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class ReservationProfile:Profile
    {
        public ReservationProfile()
        {
            CreateMap<Reservation, ReservationDto>().ReverseMap();
               
        }
        
    }
}
