namespace AW.Domain.Dto.Response;

public class CraftResponseDto : BaseCatalogResponseDto
{
    public Guid SerialId { get; set; }

    public int CraftmanId { get; set; }

    public string? CraftmanFullName { get; set; }

    public short CraftStatus { get; set; }

    public string? CraftStatusName { get; set; }

    public DateTime? PublicationDate { get; set; }

    public decimal? Price { get; set; }

    public string? History { get; set; }

    public string? CraftPictureUrl { get; set; }
}