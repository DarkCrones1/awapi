using Microsoft.EntityFrameworkCore;

using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using AW.Domain.Dto.QueryFilters;

namespace AW.Infrastructure.Repositories;

public class CustomerRepository : CrudRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(AWDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<IEnumerable<Customer>> GetPaged(Customer entity)
    {
        var query = _dbContext.Customer.AsQueryable();

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Customer>> GetPaged(CustomerQueryFilter entity)
    {
        var query = _dbContext.Customer.AsQueryable();

        return await query.ToListAsync();
    }
}