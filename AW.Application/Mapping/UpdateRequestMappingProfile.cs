using AutoMapper;
using AW.Common.Helpers;
using AW.Domain.Dto.Request.Update;
using AW.Domain.Entities;
using AW.Domain.Enumerations;

namespace AW.Application.Mapping;

public class UpdateRequestMappingProfile : Profile
{
    public UpdateRequestMappingProfile()
    {
        CreateMap<CategoryUpdateRequestDto, Category>();

        CreateMap<CraftmanUpdateRequestDto, Craftman>()
        .AfterMap(
            (src, dest) => 
            {
                var address = new Address{
                    Address1 = src.Address1,
                    Address2 = src.Address2,
                    Street = src.Street,
                    ExternalNumber = src.ExternalNumber,
                    InternalNumber = src.InternalNumber,
                    ZipCode = src.ZipCode
                };
                dest.CityId = src.CityId!;
                dest.Address.Add(address);
            }
        );
    }
}