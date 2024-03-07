using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CultureService : CatalogBaseService<Culture>, ICultureService
{
    public CultureService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Culture>> GetPaged(CultureQueryFilter filter)
    {
        var result = await _unitOfWork.CultureRepository.GetPaged(filter);
        var pagedItems = PagedList<Culture>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}