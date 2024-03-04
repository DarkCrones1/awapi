namespace AW.Domain.Dto.Response;

public class CityResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string? PhoneCode { get; set; }

    public int? CountryId { get; set; }
}