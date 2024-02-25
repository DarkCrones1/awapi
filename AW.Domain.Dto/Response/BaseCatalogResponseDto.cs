using AW.Domain.Dto.Interfaces;

namespace AW.Domain.Dto.Response;

public class BaseCatalogResponseDto : ICatalogBaseDto
{
    public int ID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; } = null;

    public string Status { get; set; } = string.Empty;

    public bool? IsActive { get; set; } = null;
}