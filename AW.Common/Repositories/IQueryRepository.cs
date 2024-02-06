using System.Linq.Expressions;
using AW.Common.Interfaces.Entities;
using AW.Common.QueryFilters;

namespace AW.Common.Interfaces.Repositories;

public interface IQueryRepository<T>  : IQueryExpresionFilterRepository<T>, IFirstOrDefaultRepository<T> where T : IBaseQueryable
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    //IEnumerable<T> GetBy(T entity);
}