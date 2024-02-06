using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface IFirstOrDefaultRepository<T> where T : IBaseQueryable
{
    Task<T> FirstOrDefault(int id);
    Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters);
    Task<T> FirstOrDefaultBy(Expression<Func<T, bool>> filters, string includeProperties);
}
