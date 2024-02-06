using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface IQueryPagedRepository<T> where T : IBaseQueryable 
{
    Task<IEnumerable<T>> GetPaged(T entity);
}