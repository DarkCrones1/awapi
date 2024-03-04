using AW.Common.Entities;
using AW.Common.Interfaces.Services;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;

namespace AW.Domain.Interfaces.Services;

public interface ICustomerService : ICrudService<Customer>
{
    Task<PagedList<Customer>> GetPaged(CustomerQueryFilter filter);
    Task UpdateProfile(int customerId, string urlProfile, string userName);
}