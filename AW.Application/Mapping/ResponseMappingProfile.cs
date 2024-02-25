using AutoMapper;
using AW.Common.Helpers;
using AW.Domain.Dto.Request.Create;
using AW.Domain.Dto.Response;
using AW.Domain.Entities;
using AW.Domain.Enumerations;

namespace AW.Application.Mapping;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap<Category, CategoryResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

        CreateMap<UserAccount, UserAccountCustomerResponseDto>()
        .ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.UserName)
        )
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        )
        .AfterMap(
            (src, dest) =>
            {
                var customer = src.Customer.FirstOrDefault() ?? new Customer();
                dest.CustomerId = customer.Id;
                dest.FullName = customer.FullName;
                dest.Phone = customer!.Phone!;
                dest.CellPhone = customer.CellPhone;

                dest.UserAccountType = src.AccountType;
                dest.UserAccountTypeName = EnumHelper.GetDescription<UserAccountType>((UserAccountType)src.AccountType);
            }
        );

        CreateMap<UserAccount, UserAccountCraftmanResponseDto>()
        .ForMember(
            dest => dest.UserName,
            opt => opt.MapFrom(src => src.UserName)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => src.IsDeleted)
        ).AfterMap(
            (src, dest) => 
            {
                var craftman = src.Craftman.FirstOrDefault() ?? new Craftman();
                dest.CraftmanId = craftman.Id;
                dest.FullName = craftman.FullName;
                dest.Phone = craftman!.Phone!;
                dest.CellPhone = craftman.CellPhone;

                dest.UserAccountType = src.AccountType;
                dest.UserAccountTypeName = EnumHelper.GetDescription<UserAccountType>((UserAccountType)src.AccountType);
            }
        );
    }
}