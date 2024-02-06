using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class CustomerType : CatalogBaseAuditablePaginationEntity
{
    public virtual ICollection<Customer> Customer { get; } = new List<Customer>();
}