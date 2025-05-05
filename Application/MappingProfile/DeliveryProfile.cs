using Application.Dtos.Delivery;
using AutoMapper;
using Domain.Entities.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class DeliveryProfile:Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Delivery,DeliveryDto>().ReverseMap();
        }
    }
}
