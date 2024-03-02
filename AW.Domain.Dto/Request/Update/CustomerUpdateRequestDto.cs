namespace AW.Domain.Dto.Request.Update;

public class CustomerUpdateRequestDto
{
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public short? Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string ZipCode { get; set; } = null!;

    public int? CityId { get; set; } = null!;

    public int? CustomerTypeId { get; set; }
}