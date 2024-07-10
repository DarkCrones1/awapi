using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CraftPictureUrl : BaseEntity
{
    public string ImageUrl { get; set; } = null!;

    public short ImageSize { get; set; }

    public int CraftId { get; set; }

    public virtual Craft Craft { get; set; } = null!;
}