using AutoMapper;
using AW.Common.Helpers;
using AW.Domain.Dto.Request.Create;
using AW.Domain.Entities;
using AW.Domain.Enumerations;

namespace Res.Application.Mapping;

public class CreateRequestMappingProfile : Profile
{
    public CreateRequestMappingProfile()
    {
        CreateMap<ImageProfileCreateRequestDto, AWDocument>();

        CreateMap<CategoryCreateRequestDto, Category>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        );

        CreateMap<CraftCreateRequestDto, Craft>()
        .ForMember(
            dest => dest.SerialId,
            opt => opt.MapFrom(src => Guid.NewGuid())
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.PublicationDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.Status,
            opt => opt.MapFrom(src => CraftStatus.Stock)
        ).ForMember(
            dest => dest.Price,
            opt => opt.MapFrom(src => src.Price)
        );

        CreateMap<TechniqueTypeCreateRequestDto, TechniqueType>()
        .ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).AfterMap(
            (src, dest) =>
            {
                foreach (var item in src.Propertys)
                {
                    var propertyTechnique = new TechniqueTypeProperty
                    {
                        CreatedDate = DateTime.Now,
                        Property = new Property
                        {
                            Name = item.Name,
                            Description = item.Description,
                            PropertyType = (short)PropertyType.TechniqueProperty,
                            IsDeleted = false,
                            CreatedDate = DateTime.Now,
                            CreatedBy = "Admin"
                        }
                    };
                    dest.Property.Add(propertyTechnique);
                }
            }
        );

        CreateMap<PropertyTechniqueTypeCreateRequestDto, Property>()
        .AfterMap(
            (src, dest) =>
            {
                dest.Name = src.Name;
                dest.Description = src.Description;
                dest.CreatedDate = DateTime.Now;
                dest.PropertyType = (short)PropertyType.TechniqueProperty;
                dest.IsDeleted = false;
                dest.CreatedBy = "Craftman";
            }
        );

        CreateMap<UserAccountCustomerCreateRequestDto, UserAccount>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.IsAuthorized,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.AccountType,
            opt => opt.MapFrom(src => (short)UserAccountType.Customer)
        ).ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(src => src.Email)
        );

        CreateMap<UserAccountCustomerCreateRequestDto, Customer>()
        .AfterMap(
            (src, dest) =>
            {
                dest.Code = Guid.NewGuid();
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;

                var customerAddress = new CustomerAddress
                {
                    RegisterDate = DateTime.Now,
                    IsDefault = true,
                    Status = 1,
                    Address = new Address
                    {
                        Address1 = "Asignar",
                        Address2 = "Asignar",
                        Street = "Asignar",
                        ExternalNumber = "Asignar",
                        InternalNumber = "Asignar",
                        ZipCode = "Asignar",
                        CityId = null
                    }
                };
                dest.CustomerAddress.Add(customerAddress);
            }
        );

        CreateMap<UserAccountCraftmanCreateRequestDto, UserAccount>()
        .ForMember(
            dest => dest.IsDeleted,
            opt => opt.MapFrom(src => ValuesStatusPropertyEntity.IsNotDeleted)
        ).ForMember(
            dest => dest.IsActive,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.IsAuthorized,
            opt => opt.MapFrom(src => true)
        ).ForMember(
            dest => dest.CreatedDate,
            opt => opt.MapFrom(src => DateTime.Now)
        ).ForMember(
            dest => dest.AccountType,
            opt => opt.MapFrom(src => (short)UserAccountType.Craftman)
        ).ForMember(
            dest => dest.Email,
            opt => opt.MapFrom(src => src.Email)
        );

        CreateMap<UserAccountCraftmanCreateRequestDto, Craftman>()
        .AfterMap(
            (src, dest) =>
            {
                dest.Code = Guid.NewGuid();
                dest.IsDeleted = ValuesStatusPropertyEntity.IsNotDeleted;
                dest.CreatedDate = DateTime.Now;
                dest.Status = (short)CraftmanStatus.Aproved;
                var address = new Address
                {
                    Address1 = "Asignar",
                    Address2 = "Asignar",
                    Street = "Asignar",
                    ExternalNumber = "Asignar",
                    InternalNumber = "Asignar",
                    ZipCode = "Asignar",
                    CityId = null
                };
                dest.Address.Add(address);
            }
        );
    }
}