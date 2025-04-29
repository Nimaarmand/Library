using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region YourEntityClass Mapping

            // CreateMap<YourEntityClass, YourEntityClassDto>().ReverseMap();

            #endregion

        }
    }

public static class AutoMapperExtensions
{
    public static TDestination Map<TDestination>(this IMapper mapper, params object[] source) where TDestination : class
    {
        TDestination destination = mapper.Map<TDestination>(source.FirstOrDefault());

        foreach (var src in source.Skip(1))
            destination = mapper.Map(src, destination);

        return destination;
    }

    public static TDestination Map<TDestination>(this IMapper mapper, TDestination destination, params object[] source) where TDestination : class
    {
        foreach (var src in source)
            destination = mapper.Map(src, destination);

        return destination;
    }
}

}
