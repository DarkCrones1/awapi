using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CartCraft : BaseEntity
{
    public int CartId { get; set; }

    public int CraftId { get; set; }

    public int AmountItems { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Craft Craft { get; set; } = null!;
}