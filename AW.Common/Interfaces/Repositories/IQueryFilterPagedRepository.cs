using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface IQueryFilterPagedRepository<T, E> : IQueryPagedRepository<T> where T : IBaseQueryable where E : IBaseQueryFilter
{
    Task<IEnumerable<T>> GetPaged(E filter);
}