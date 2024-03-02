using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Craftman : BaseRemovableAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public string CellPhone { get; set; } = null!;

    public short Status { get; set; }

    public int? CityId { get; set; }

    public virtual ICollection<Address> Address { get; } = new List<Address>();

    public virtual ICollection<Craft> Craft { get; } = new List<Craft>();

    public virtual ICollection<CraftmanDocument> CraftmanDocument { get; } = new List<CraftmanDocument>();

    public virtual City City { get; set; } = null!;

    public virtual ICollection<Sale> Sale { get; } = new List<Sale>();

    public virtual ICollection<TechniqueType> TechniqueType { get; } = new List<TechniqueType>();

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}