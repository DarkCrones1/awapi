using Microsoft.Extensions.Configuration;

using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
// using AW.Domain.Interfaces.Repositories;

// using Res.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;

namespace AW.Infrastructure.Repositories;

public class UnirOfWork : IUnitOfWork
{
    protected readonly AWDbContext _dbContext;

    private readonly IConfiguration _configuration;

    

    private bool disposed;

    public UnirOfWork(AWDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        disposed = false;

        
    }

    

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
            if (disposing)
                _dbContext.Dispose();

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}