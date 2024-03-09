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
        CreateMap<Cart, CartResponseDto>()
        .ForMember(
            dest => dest.StatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CartStatus>((CartStatus)src.Status))
        );

        CreateMap<Cart, CartDetailResponseDto>()
        .ForMember(
            dest => dest.Craft,
            opt => opt.MapFrom(src => src.Craft)
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => src.Status)
        ).ForMember(
            dest => dest.StatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CartStatus>((CartStatus)src.Status))
        );

        CreateMap<Category, CategoryResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

        CreateMap<City, CityResponseDto>();

        CreateMap<Country, CountryResponseDto>();

        CreateMap<Craft, CraftResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.CraftStatus,
            opt => opt.MapFrom(src => src.Status)
        ).ForMember(
            dest => dest.CraftStatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CraftStatus>((CraftStatus)src.Status))
        );

        CreateMap<Craft, CraftDetailResponseDto>()
        .ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.CraftStatus,
            opt => opt.MapFrom(src => src.Status)
        ).ForMember(
            dest => dest.CraftStatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CraftStatus>((CraftStatus)src.Status))
        );

        CreateMap<Craftman, CraftmanResponseDto>()
        .ForMember(
            dest => dest.StatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CraftmanStatus>((CraftmanStatus)src.Status))
        ).ForMember(
            dest => dest.GenderName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<Gender>((Gender)src.Gender!))
        );

        CreateMap<Craftman, CraftmanDetailResponseDto>()
        .ForMember(
            dest => dest.StatusName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<CraftmanStatus>((CraftmanStatus)src.Status))
        ).ForMember(
            dest => dest.GenderName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<Gender>((Gender)src.Gender!))
        ).AfterMap(
            (src, dest) =>
            {
                var craftmanAddress = src.Address.FirstOrDefault() ?? new Address();
                var craftmanCity = craftmanAddress.City ?? new City();

                dest.Address1 = craftmanAddress.Address1;
                dest.Address2 = craftmanAddress.Address2;
                dest.Street = craftmanAddress.Street;
                dest.ExternalNumber = craftmanAddress.ExternalNumber;
                dest.InternalNumber = craftmanAddress.InternalNumber;
                dest.ZipCode = craftmanAddress.ZipCode;
                dest.CityId = craftmanAddress.CityId;
                dest.CityName = craftmanCity.Name;
                dest.FullAddress = craftmanAddress.FullAddress;
            }
        );

        CreateMap<Culture, CultureResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        );

        CreateMap<Customer, CustomerResponseDto>()
        .ForMember(
            dest => dest.GenderName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<Gender>((Gender)src.Gender!))
        );

        CreateMap<Customer, CustomerDetailResponseDto>()
        .ForMember(
            dest => dest.GenderName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<Gender>((Gender)src.Gender!))
        )
        .AfterMap(
            (src, dest) =>
            {

            }
        );

        CreateMap<Property, PropertyResponseDto>()
        .ForMember(
            dest => dest.PropertyTypeName,
            opt => opt.MapFrom(src => EnumHelper.GetDescription<PropertyType>((PropertyType)src.PropertyType))
        )
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        );

        CreateMap<CraftProperty, PropertyResponseDto>()
        .AfterMap(
            (src, dest) =>
            {
                var craft = src.Craft;
                var property = src.Property;

                dest.Id = src.Id;
                dest.Name = property.Name;
                dest.Description = property.Description;
                dest.IsActive = !property.IsDeleted;
                dest.Status = StatusDeletedHelper.GetStatusDeletedEntity(property.IsDeleted);
                dest.PropertyType = property.PropertyType;
                dest.PropertyTypeName = EnumHelper.GetDescription<PropertyType>((PropertyType)property.PropertyType);
            }
        );

        CreateMap<TechniqueTypeProperty, PropertyResponseDto>()
        .AfterMap(
            (src, dest) =>
            {
                var techniqueType = src.TechniqueType;
                var property = src.Property;

                dest.Id = src.Id;
                dest.Name = property.Name;
                dest.Description = property.Description;
                dest.IsActive = !property.IsDeleted;
                dest.Status = StatusDeletedHelper.GetStatusDeletedEntity(property.IsDeleted);
                dest.PropertyType = property.PropertyType;
                dest.PropertyTypeName = EnumHelper.GetDescription<PropertyType>((PropertyType)property.PropertyType);
            }
        );

        CreateMap<TechniqueType, TechniqueTypeResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
        );

        CreateMap<TechniqueType, TechniqueTypeDetailResponseDto>()
        .ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => StatusDeletedHelper.GetStatusDeletedEntity(src.IsDeleted))
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => !src.IsDeleted)
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