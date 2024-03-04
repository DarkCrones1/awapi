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

    public Task DeteleAsync(LocalContainer container, string localFileName)
    {
        throw new NotImplementedException();
    }

    public Task<string> EditFileAsync(IFormFile file, LocalContainer container)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadAsync(IFormFile file, LocalContainer container, string localFileName)
    {
        return await _unitOfWork.LocalStorageRepository.UploadAsync(file, container, localFileName);
    }
}