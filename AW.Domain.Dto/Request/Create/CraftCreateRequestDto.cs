namespace AW.Domain.Dto.Request.Create;

public class CraftCreateRequestDto : BaseCatalogCreateRequestDto
{
    private IEnumerable<PropertyCraftCreateRequestDto> _Propertys;

    public CraftCreateRequestDto()
    {
        _Propertys = new List<PropertyCraftCreateRequestDto>();
    }

    public int CraftmanId { get; set; }

    public decimal? Price { get; set; }

    public string? History { get; set; }

    public string? CraftPictureUrl { get; set; }

    public int[]? CategoryIds { get; set; }

    public int[]? TechniqueIds { get; set; }

    public IEnumerable<PropertyCraftCreateRequestDto> Propertys { get => _Propertys; set => _Propertys = value ?? new List<PropertyCraftCreateRequestDto>(); }
}