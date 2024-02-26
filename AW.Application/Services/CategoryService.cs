using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CategoryService : CatalogBaseService<Category>, ICategoryService
{
    public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Category>> GetPaged(CategoryQueryFilter filter)
    {
        var result = await _unitOfWork.CategoryRepository.GetPaged(filter);
        var pagedItems = PagedList<Category>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }
}