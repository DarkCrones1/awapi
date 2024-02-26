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
    }
}