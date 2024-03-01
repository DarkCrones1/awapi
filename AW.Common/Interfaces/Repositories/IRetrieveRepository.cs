using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface IRetrieveRepository<T> : IQueryRepository<T>, IQueryPagedRepository<T> where T : IBaseQueryable
{

}