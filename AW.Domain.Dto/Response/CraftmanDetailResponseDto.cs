namespace AW.Domain.Dto.Response;

public class CraftmanDetailResponseDto
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public short? Gender { get; set; }

    public string? GenderName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string ZipCode { get; set; } = null!;

    public int? CityId { get; set; }

    public string CityName = string.Empty;

    public string? FullAddress { get; set; }

    public IEnumerable<CraftResponseDto> Craft { get; } = new List<CraftResponseDto>();
}