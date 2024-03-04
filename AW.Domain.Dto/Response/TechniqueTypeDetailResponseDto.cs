namespace AW.Domain.Dto.Response;

public class TechniqueTypeDetailResponseDto : BaseCatalogResponseDto
{
    public IEnumerable<PropertyResponseDto> Property { get; } = new List<PropertyResponseDto>();
}