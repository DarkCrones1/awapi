using AW.Common.Interfaces.Repositories;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Repositories;

public interface ICartRepository : ICrudRepository<Cart>, IQueryFilterPagedRepository<Cart, CartQueryFilter>
{
}