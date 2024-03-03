namespace AW.Domain.Dto.Response;

public class CustomerDetailResponseDto
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string? Name { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public int? CustomerTypeId { get; set; }

    public string? CustomerTypeName { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public int? CityId { get; set; }

    public string? CityName { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public short? Gender { get; set; }

    public string GenderName { get; set; } = null!;

    public DateTime? BirthDate { get; set; }

    public int Age
    {
        get
        {
            if (!BirthDate.HasValue) return 0;

            DateTime endDate = DateTime.Now;
            TimeSpan difference = endDate - BirthDate.Value;
            int yearsDifference = (int)(difference.TotalDays / 365.25);

            return yearsDifference;
        }
    }

    public short? Status { get; set; }

    public string? StatusName { get; set; }

    public bool? IsDeleted { get; set; }
}