using System.Linq.Expressions;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface IQueryExpresionFilterRepository<T> where T : IBaseQueryable
{
    Task<IEnumerable<T>> GetBy(Expression<Func<T, bool>> filters, string includeProperties = "");

    IQueryable<T> Get(Expression<Func<T, bool>>? filters = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
}