using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class UserDataInfo : BaseAuditableEntity
{
    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string CellPhone { get; set; } = null!;

    public string? Phone { get; set; }

    public string? ProfilePictureUrl { get; set; }

    public DateTime? BirthDate { get; set; }

    public short? Gender { get; set; }

    public string FullName { get => $"{FirstName} {MiddleName} {LastName}".Trim(); }

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}