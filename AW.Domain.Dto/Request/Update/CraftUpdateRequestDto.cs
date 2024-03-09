namespace AW.Domain.Dto.Request.Update;

public class CraftUpdateRequestDto : BaseCatalogUpdateRequestDto
{
    public decimal? Price { get; set; }

    public string? History { get; set; }

    public int CultureId { get; set; }

    public int[]? CategoryIds { get; set; }

    public int[]? TechniqueTypeIds { get; set; }
}