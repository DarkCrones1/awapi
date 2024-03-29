using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class ActiveUserAccountAdministrator : BaseQueryable
{
    public string UserName { get; set; } = null!;

    public int AdministratorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public short AccountType { get; set; }

    public int RolId { get; set; }

    public string? RolName { get; set; }

    public string Name => $"{this.FirstName} {this.MiddleName} {this.LastName}".Trim();
}