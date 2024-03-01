using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AW.Common.Entities;

namespace AW.Common.Interfaces.Repositories;

public interface ICatalogBaseRepository<T> : ICrudRepository<T>, IQueryPagedRepository<T> where T : CatalogBaseEntity
{

}
