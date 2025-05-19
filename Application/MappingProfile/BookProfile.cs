using Application.Dtos.Books;
using AutoMapper;
using Domain.Entities.Books;

namespace Application.MappingProfile
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
           
            CreateMap<BookCategories, BookCategoriesDto>().ReverseMap();

            CreateMap<BookDto, Book>().ReverseMap()
            .ForMember(dest => dest.BookCategories, opt => opt.Ignore())
            .ForMember(dest => dest.PublicationDate, opt => opt.MapFrom(src => src.PublicationDate));

            //  .ForMember(dest => dest.BaseInfo, opt => opt.MapFrom(src => new BaseDto
            //  {
            //      InsertTime = src.InsertTime.ConvertToShamsi(),
            //      UpdateTime = src.UpdateTime.HasValue ? src.UpdateTime.Value.ConvertToShamsi() : null,
            //      IsActive = src.IsActive,
            //})).ReverseMap();
        }
    }
}
