namespace AW.Domain.Dto.Response;

public class CraftmanResponseDto
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

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }
}