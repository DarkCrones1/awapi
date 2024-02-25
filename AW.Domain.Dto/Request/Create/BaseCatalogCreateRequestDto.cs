using AW.Domain.Dto.Interfaces;

namespace AW.Domain.Dto.Request.Create;

public class BaseCatalogCreateRequestDto : ICatalogBaseDto
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;
}