namespace AW.Domain.Dto.Response;

public class PropertyResponseDto : BaseCatalogResponseDto
{
    public short PropertyType { get; set; }

    public string? PropertyTypeName { get; set; }
}