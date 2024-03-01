using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Enumerations;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CraftmanService : CrudService<Craftman>, ICraftmantService
{
    public CraftmanService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Craftman>> GetPaged(CraftmanQueryFilter filter)
    {
        var result = await _unitOfWork.CraftmanRepository.GetPaged(filter);
        var pagedItems = PagedList<Craftman>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public override async Task Update(Craftman entity)
    {
        var oldEntity = await GetById(entity.Id);
        var address = oldEntity.Address.FirstOrDefault()!;

        var newAddress = entity.Address.FirstOrDefault()!;
        address.Id = address.Id;
        address.Address1 = newAddress.Address1;
        address.Address2 = newAddress.Address2;
        address.Street = newAddress.Street;
        address.ExternalNumber = newAddress.ExternalNumber;
        address.InternalNumber = newAddress.InternalNumber;
        address.ZipCode = newAddress.ZipCode;

        _unitOfWork.AddressRepository.Update(address);
        
        entity.Code = oldEntity.Code;
        entity.Status = (short)CraftmanStatus.Aproved;
        await base.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateProfile(int craftmanId, string urlProfile)
    {
        var lastEntity = await _unitOfWork.CraftmanRepository.GetById(craftmanId);

        lastEntity.ProfilePictureUrl = urlProfile;
        lastEntity.LastModifiedDate = DateTime.Now;

        _unitOfWork.CraftmanRepository.Update(lastEntity);
        await _unitOfWork.SaveChangesAsync();
    }
}