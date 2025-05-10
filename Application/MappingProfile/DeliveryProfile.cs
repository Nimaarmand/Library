using Application.Dtos.Delivery;
using AutoMapper;
using Domain.Entities.Reservations;

namespace Application.MappingProfile
{
    public class DeliveryProfile : Profile
    {
        public DeliveryProfile()
        {
            CreateMap<Deliverys, DeliveryDto>().ReverseMap();
        }
    }
}
