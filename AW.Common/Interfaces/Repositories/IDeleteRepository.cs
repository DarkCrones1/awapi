using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;
public interface IDeleteRepository<T> where T : IBaseQueryable
{
    Task<int> Delete(int id);
    Task<int> DeleteRange(IEnumerable<int> idList);
    Task<int> DeleteBy(Expression<Func<T, bool>> filter);
}
