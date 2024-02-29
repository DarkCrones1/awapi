namespace AW.Domain.Dto.Response;

public class CountryResponseDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public bool? IsDeleted { get; set; }
}