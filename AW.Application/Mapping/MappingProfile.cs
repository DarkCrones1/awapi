using AutoMapper;
using AW.Domain.Dto.Request.Create;


// using AW.Domain.Dto.Request.Create;

using AW.Domain.Entities;
using AW.Domain.Enumerations;
// using AW.Domain.Enumerations;

namespace AW.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BaseCatalogCreateRequestDto, Rol>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        )
        .AfterMap(
            (src, dest, context) =>
            {

            }
        );
    }
}

