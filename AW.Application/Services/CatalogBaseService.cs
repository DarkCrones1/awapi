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

        if (typeRep == typeof(Category))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CategoryRepository;

        if (typeRep == typeof(Property))
            return (ICatalogBaseRepository<T>)this._unitOfWork.PropertyRepository;

        if (typeRep == typeof(Craft))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CraftmanRepository;

        if (typeRep == typeof(Culture))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CultureRepository;

        if (typeRep == typeof(CustomerType))
            return (ICatalogBaseRepository<T>)this._unitOfWork.CustomerTypeRepository;

        if (typeRep == typeof(TechniqueType))
            return (ICatalogBaseRepository<T>)this._unitOfWork.TechniqueTypeRepository;

        return (ICatalogBaseRepository<T>)this._unitOfWork.RolRepository;
    }
}