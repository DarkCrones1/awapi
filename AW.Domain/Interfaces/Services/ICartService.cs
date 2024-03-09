using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ICartService : ICrudService<Cart>
{
    Task<PagedList<Cart>> GetPaged(CartQueryFilter filter);
    Task CreateCart(Cart entity, int[] CraftIds);
}