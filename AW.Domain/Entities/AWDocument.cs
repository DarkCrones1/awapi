using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class AWDocument : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Extension { get; set; } = null!;

    public short Type { get; set; }

    public string UrlDocument { get; set; } = null!;

    public virtual ICollection<CustomerDocument> CustomerDocument { get; } = new List<CustomerDocument>();

    public virtual ICollection<CraftmanDocument> CraftmanDocument { get; } = new List<CraftmanDocument>();
}