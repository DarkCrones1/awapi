using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Craftman : BaseRemovableAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public short Status { get; set; }

    public string? History { get; set; }

    public virtual ICollection<Address> Address { get; } = new List<Address>();

    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();

    public virtual ICollection<CraftmanDocument> CraftmanDocument { get; } = new List<CraftmanDocument>();

    public virtual ICollection<Sale> Sale { get; } = new List<Sale>();

    public virtual ICollection<TechniqueType> TechniqueType { get; } = new List<TechniqueType>();

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}