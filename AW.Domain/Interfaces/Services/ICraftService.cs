using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ICraftService : ICatalogBaseService<Craft>
{
    Task<PagedList<Craft>> GetPaged(CraftQueryFilter filter);
    Task UpdateProfile(CraftPictureUrl craftPictureUrl, string userName);
    Task CreateCraft(Craft entity, int[] CategoryIds, int[] TechniqueTypeIds);
    Task UpdateCraft(Craft entity, int[] CategoryIds, int[] TechniqueTypeIds);
}