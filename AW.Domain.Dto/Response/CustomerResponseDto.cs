namespace AW.Domain.Dto.Response;

public class CustomerResponseDto
{
    public int Id { get; set; }

    public Guid Code { get; set; }

    public string? Name { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

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