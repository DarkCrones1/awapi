namespace AW.Domain.Dto.Response;

public class UserAccountAdministratorResponseDto
{
    public int Id { get; set; }

    public int AdministratorId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string CellPhone { get; set; } = string.Empty;

    public short UserAccountType { get; set; }

    public string? UserAccountTypeName { get; set; }

    public int RolId { get; set; }

    public string? RolName { get; set; }

    public bool IsDeleted { get; set; }
}