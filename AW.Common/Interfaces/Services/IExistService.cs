using System.Linq.Expressions;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Services;

    public interface IExistService<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
