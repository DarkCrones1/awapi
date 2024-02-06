using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Entities;
using System.Linq.Expressions;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

    public interface IExistRepository<T> where T : IBaseQueryable
    {
        Task<bool> Exist(Expression<Func<T, bool>> filters);
    }
