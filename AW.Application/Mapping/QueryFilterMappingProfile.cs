using AutoMapper;

using AW.Common.QueryFilters;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Application.Mapping;

public class QueryFilterMappingProfile : Profile
{
    public QueryFilterMappingProfile()
    {
        CreateMap<BaseCatalogQueryFilter, Category>();

        CreateMap<CartQueryFilter, Cart>();

        CreateMap<CategoryQueryFilter, Category>();

        CreateMap<CityQueryFilter, City>();

        CreateMap<CountryQueryFilter, Country>();

        CreateMap<CraftQueryFilter, Craft>();

        CreateMap<CraftmanQueryFilter, Craftman>();

        CreateMap<CultureQueryFilter, Culture>();

        CreateMap<CustomerQueryFilter, Customer>();

        CreateMap<TechniqueQueryFilter, TechniqueType>();

        CreateMap<TicketQueryFilter, Ticket>();

        CreateMap<UserAccountQueryFilter, UserAccount>();
    }
}