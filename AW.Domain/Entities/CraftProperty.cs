using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CraftProperty : BaseEntity
{
    public int CraftId { get; set; }

    public int PropertyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Property Property { get; set; } = null!;

    public virtual Craft Craft { get; set; } = null!;
}