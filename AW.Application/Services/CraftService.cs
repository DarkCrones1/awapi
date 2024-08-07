using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Enumerations;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CraftService : CatalogBaseService<Craft>, ICraftService
{
    public CraftService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Craft>> GetPaged(CraftQueryFilter filter)
    {
        var result = await _unitOfWork.CraftRepository.GetPaged(filter);
        var pagedItems = PagedList<Craft>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public async Task UpdateProfile(CraftPictureUrl craftPictureUrl, string userName)
    {
        var lastEntity = await _unitOfWork.CraftRepository.GetById(craftPictureUrl.CraftId);

        var picture = new CraftPictureUrl
        {
            CraftId = craftPictureUrl.CraftId,
            ImageSize = craftPictureUrl.ImageSize,
            ImageUrl = craftPictureUrl.ImageUrl,
        };
        lastEntity.CraftPictureUrl.Add(picture);
        lastEntity.LastModifiedDate = DateTime.Now;
        lastEntity.LastModifiedBy = userName;

        await base.Update(lastEntity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateCraft(Craft entity, int[] CategoryIds, int[] TechniqueTypeIds)
    {
        foreach (var item in CategoryIds)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(item);
            if (item > 0)
                entity.Category.Add(category);
        }

        foreach (var item in TechniqueTypeIds)
        {
            var technique = await _unitOfWork.TechniqueTypeRepository.GetById(item);
            if (item > 0)
                entity.TechniqueType.Add(technique);
        }

        await base.Create(entity);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateCraft(Craft entity, int[] CategoryIds, int[] TechniqueTypeIds)
    {
        var oldEntity = await _unitOfWork.CraftRepository.GetById(entity.Id);

        oldEntity.Category.Clear();

        foreach (var item in CategoryIds)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(item);
            if (item > 0)
                oldEntity.Category.Add(category);
        }

        oldEntity.TechniqueType.Clear();

        foreach (var item in TechniqueTypeIds)
        {
            var technique = await _unitOfWork.TechniqueTypeRepository.GetById(item);
            if (item > 0)
                oldEntity.TechniqueType.Add(technique);
        }

        entity.CraftmanId = oldEntity.CraftmanId;
        entity.SerialId = oldEntity.SerialId;
        entity.PublicationDate = oldEntity.PublicationDate;
        entity.Status = (short)CraftStatus.Stock;

        await base.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}