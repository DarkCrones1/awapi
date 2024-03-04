using AW.Common.Enumerations;
using AW.Common.Interfaces.Services;
using AW.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AW.Application.Services;

public class LocalStorageService : ILocalStorageService
{
    private readonly IUnitOfWork _unitOfWork;

    public LocalStorageService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task DeteleAsync(LocalContainer container, string route)
    {
        await _unitOfWork.LocalStorageRepository.DeteleAsync(container, route);
    }

    public async Task<string> EditFileAsync(IFormFile file, LocalContainer container)
    {
        return await _unitOfWork.LocalStorageRepository.EditFileAsync(file, container);
    }

    public async Task<string> UploadAsync(IFormFile file, LocalContainer container, string localFileName)
    {
        return await _unitOfWork.LocalStorageRepository.UploadAsync(file, container, localFileName);
    }
}