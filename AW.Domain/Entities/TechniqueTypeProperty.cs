using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class TechniqueTypeProperty : BaseEntity
{
    public int TechniqueTypeId { get; set; }

    public int PropertyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Property Property { get; set; } = null!;

    public virtual TechniqueType TechniqueType { get; set; } = null!;
}