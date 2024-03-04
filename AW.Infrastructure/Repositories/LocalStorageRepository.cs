using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

using AW.Common.Enumerations;
using AW.Common.Interfaces.Repositories;

namespace AW.Infrastructure.Repositories;

public class LocalStorageRepository : ILocalStorageRepository
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LocalStorageRepository(IConfiguration configuration, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        this._configuration = configuration;
        this._env = env;
        this._httpContextAccessor = httpContextAccessor;
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
        if (file.Length == 0) return null!;

        // Nombre del contenedor
        var containerName = Enum.GetName(typeof(LocalContainer), container)!.ToLower();
        containerName = containerName.Replace("_", "-");

        // Ruta de la carpeta
        var folder = Path.Combine(_env.WebRootPath, containerName);

        // Si no existe la carpeta, la crea
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        // // Si el nombre del archivo va vacio, se crea uno nuevo
        // if (string.IsNullOrEmpty(localFileName))
        // {
        //     localFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
        // }

        localFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        // Ruta de la iamgen
        var filePath = Path.Combine(folder, localFileName);

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            await File.WriteAllBytesAsync(filePath, memoryStream.ToArray());
        }
        // await File.WriteAllBytesAsync(uploadPath, );
        var urlActually = $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";

        var urlDB = Path.Combine(urlActually, containerName, localFileName).Replace("\\", "/");
        return urlDB;
    }
}