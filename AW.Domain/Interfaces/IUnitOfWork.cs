using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
// using AW.Domain.Interfaces.Repositories;

namespace AW.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    void SaveChanges();

    Task SaveChangesAsync();
}