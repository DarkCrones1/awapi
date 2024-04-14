using AW.Common.QueryFilters;

namespace AW.Domain.Dto.QueryFilters;

public class CraftQueryFilter : BaseCatalogQueryFilter
{
    public Guid? SerialId { get; set; }

    public string? InitialPartSerialId { get; set; }

    public int CraftmanId { get; set; }

    public string? CraftmanName { get; set; }

    public short CraftStatus { get; set; }

    public int CultureId { get; set; }

    public DateTime? PublicationDate { get; set; }

    public DateTime? MinPublicationDate { get; set; }

    public DateTime? MaxPublicationDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? MinCreatedDate { get; set; }

    public DateTime? MaxCreatedDate { get; set; }

    public decimal Price { get; set; }

    public decimal MinPrice { get; set; }

    public decimal MaxPrice { get; set; }

    public int[]? CategoryIds { get; set; }

    public int[]? TechniqueTypeIds { get; set; }
}