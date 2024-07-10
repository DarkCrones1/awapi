namespace AW.Domain.Dto.Response;

public class CraftPictureUrlResponseDto
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public short ImageSize { get; set; }

    public string ImageSizeName { get; set; } = null!;

    public int CraftId { get; set; }
}