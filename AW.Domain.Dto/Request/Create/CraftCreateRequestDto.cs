namespace AW.Domain.Dto.Request.Create;

public class CraftCreateRequestDto : BaseCatalogCreateRequestDto
{
    public int CraftmanId { get; set; }

    public int[]? CategoryIds { get; set; }

    public int[]? TechniqueIds { get; set; }
}