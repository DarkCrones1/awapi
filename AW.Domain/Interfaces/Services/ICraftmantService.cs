using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ICraftmantService : ICrudService<Craftman>
{
    Task<PagedList<Craftman>> GetPaged(CraftmanQueryFilter filter);
    Task UpdateProfile(int craftmanId, string urlProfile, string userName);
    Task DownedProfile(Craftman entity);
}