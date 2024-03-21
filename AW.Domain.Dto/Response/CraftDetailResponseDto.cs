namespace AW.Domain.Dto.Response;

public class CraftDetailResponseDto : BaseCatalogResponseDto
{
    private IEnumerable<CategoryResponseDto> _category;
    private IEnumerable<TechniqueTypeResponseDto> _techniqueType;

    public CraftDetailResponseDto()
    {
        _category = new List<CategoryResponseDto>();
        _techniqueType = new List<TechniqueTypeResponseDto>();
    }

    public Guid SerialId { get; set; }

    public int CraftmanId { get; set; }

    public string? CraftmanFullName { get; set; }

    public short CraftStatus { get; set; }

    public string? CraftStatusName { get; set; }

    public int CultureId { get; set; }

    public string? CultureName { get; set; }

    public DateTime? PublicationDate { get; set; }

    public decimal? Price { get; set; }

    public string? History { get; set; }

    public string? CraftPictureUrl { get; set; }

    public IEnumerable<CategoryResponseDto> Category { get => _category; set => _category = value; }

    public IEnumerable<TechniqueTypeResponseDto> TechniqueType { get => _techniqueType; set => _techniqueType = value; }

    public IEnumerable<PropertyResponseDto> Property { get; } = new List<PropertyResponseDto>();
}