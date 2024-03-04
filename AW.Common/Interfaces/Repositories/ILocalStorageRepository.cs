using AW.Common.Enumerations;
using Microsoft.AspNetCore.Http;

namespace AW.Common.Interfaces.Repositories;

public interface ILocalStorageRepository
{
    Task<string> EditFileAsync(IFormFile file, LocalContainer container);
    Task DeteleAsync(LocalContainer container, string route);
    Task<string> UploadAsync(IFormFile file, LocalContainer container, string localFileName);
}