using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Administrator : BaseRemovableAuditablePaginationEntity
{
    public int JobId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}