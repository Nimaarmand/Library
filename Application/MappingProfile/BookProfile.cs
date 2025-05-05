using Application.Dtos.Books;
using Application.Dtos.Commons;
using AutoMapper;
using Domain.Entities.Books;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookCategories, BookCategoriesDto>().ReverseMap();

            //  .ForMember(dest => dest.BaseInfo, opt => opt.MapFrom(src => new BaseDto
            //  {
            //      InsertTime = src.InsertTime.ConvertToShamsi(),
            //      UpdateTime = src.UpdateTime.HasValue ? src.UpdateTime.Value.ConvertToShamsi() : null,
            //      IsActive = src.IsActive,
            //})).ReverseMap();
        }
    }
}
