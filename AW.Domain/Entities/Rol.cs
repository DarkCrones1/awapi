using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Rol : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}