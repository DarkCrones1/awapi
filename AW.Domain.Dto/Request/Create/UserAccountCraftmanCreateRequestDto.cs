namespace AW.Domain.Dto.Request.Create;

public class UserAccountCraftmanCreateRequestDto
{
    // UserAccount
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    // Customer info
    // public string FirstName { get; set; } = null!;

    // public string? MiddleName { get; set; }

    // public string LastName { get; set; } = null!;

    // public string CellPhone { get; set; } = null!;

    // public string? Phone { get; set; }

    // public short? Gender { get; set; }

    // public DateTime? BirthDate { get; set; }
}