using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ICategoryService : ICatalogBaseService<Category>
{
    Task<PagedList<Category>> GetPaged(CategoryQueryFilter filter);
    Task UpdateProfile(int craftmanId, string urlProfile, string userName);
}