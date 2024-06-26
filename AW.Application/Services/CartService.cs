using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CartService : CrudService<Cart>, ICartService
{
    public CartService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Cart>> GetPaged(CartQueryFilter filter)
    {
        var result = await _unitOfWork.CartRepository.GetPaged(filter);
        var pagedItems = PagedList<Cart>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public async Task CreateCart(Cart entity)
    {
        await base.Create(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}