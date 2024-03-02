using AW.Common.Interfaces.Repositories;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Repositories;

public interface ICustomerRepository : ICrudRepository<Customer>, IQueryFilterPagedRepository<Customer, CustomerQueryFilter>
{
}