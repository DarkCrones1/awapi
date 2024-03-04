using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class TechniqueTypeService : CatalogBaseService<TechniqueType>, ITechniqueTypeService
{
    public TechniqueTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<TechniqueType>> GetPaged(TechniqueQueryFilter filter)
    {
        var result = await _unitOfWork.TechniqueTypeRepository.GetPaged(filter);
        var pagedItems = PagedList<TechniqueType>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}