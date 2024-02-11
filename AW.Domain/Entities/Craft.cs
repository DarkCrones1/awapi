using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Craft : CatalogBaseAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int CraftmanId { get; set; }

    public short Status { get; set; }

    public DateTime? PublicationDate { get; set; }

    public virtual ICollection<Category> Category { get; } = new List<Category>();

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<TechniqueType> TechniqueType { get; } = new List<TechniqueType>();

    public virtual ICollection<Sale> Sale { get; } = new List<Sale>();
}