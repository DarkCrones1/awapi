using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Craft : CatalogBaseAuditablePaginationEntity
{
    public Guid SerialId { get; set; }

    public int CraftmanId { get; set; }

    public int? CultureId { get; set; }

    public short Status { get; set; }

    public DateTime? PublicationDate { get; set; }

    public virtual ICollection<Category> Category { get; } = new List<Category>();

    public virtual ICollection<CartCraft> CartCraft { get; } = new List<CartCraft>();

    public virtual Craftman Craftman { get; set; } = null!;

    public virtual Culture Culture { get; set; } = null!;

    public virtual ICollection<CraftProperty> Property { get; } = new List<CraftProperty>();

    public virtual ICollection<TechniqueType> TechniqueType { get; } = new List<TechniqueType>();

    public virtual ICollection<Sale> Sale { get; } = new List<Sale>();
}