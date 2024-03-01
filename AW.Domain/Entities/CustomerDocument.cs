using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CustomerDocument : BaseEntity
{
    public int CustomerId { get; set; }

    public int DocumentId { get; set; }

    public string Value { get; set; } = null!;

    public DateTime ExpirationDate { get; set; }

    public short CustomerDocumentTypeId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual AWDocument Document { get; set; } = null!;
}