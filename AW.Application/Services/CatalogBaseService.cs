using AW.Common.Entities;
using AW.Common.Interfaces.Entities;
using AW.Common.Interfaces.Repositories;
using AW.Common.Interfaces.Services;
using AW.Domain.Entities;
using AW.Domain.Interfaces;

namespace AW.Application.Services;

public class CatalogBaseService<T> : CrudService<T>, ICatalogBaseService<T> where T : CatalogBaseEntity
{
    protected new ICatalogBaseRepository<T> _repository;
    public CatalogBaseService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        this._repository = GetRepository();
    }

    protected override ICatalogBaseRepository<T> GetRepository()
    {
        var typeRep = typeof(T);

        return null;
    }
}