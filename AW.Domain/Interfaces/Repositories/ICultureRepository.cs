using System.Linq.Expressions;

using AW.Common.Interfaces.Repositories;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Repositories;

public interface ICultureRepository : ICatalogBaseRepository<Culture>, IQueryFilterPagedRepository<Culture, CultureQueryFilter>
{
}