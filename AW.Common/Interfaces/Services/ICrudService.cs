using AW.Common.Interfaces.Entities;

namespace AW.Common.Interfaces.Services;

public interface ICrudService<T> : IQueryService<T>, ICreateService<T>, IUpdateService<T>, IDeleteService<T>, IExistService<T> where T : IBaseQueryable
{
    T MapCurrentEntityToUpdate(T source, T target);
}