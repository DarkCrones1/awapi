using AW.Common.Enumerations;
using AW.Common.Interfaces.Services;
using AW.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace gem.application.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
    private readonly IUnitOfWork _unitOfWork;

    public AzureBlobStorageService(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
    public async Task DeleteAsync(AzureContainer container, string blobFilename)
    {
        await _unitOfWork.AzureBlobStorageRepository.DeleteAsync(container, blobFilename);
    }

    public async Task<string> UploadAsync(IFormFile file, AzureContainer container, string blobFilename)
    {
        return await _unitOfWork.AzureBlobStorageRepository.UploadAsync(file, container, blobFilename);
    }
}