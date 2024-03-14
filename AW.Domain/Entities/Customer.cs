using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Customer : BaseRemovableAuditablePaginationEntity
{
    public Guid Code { get; set; }

    public int? CustomerTypeId { get; set; }

    public virtual ICollection<Cart> Cart { get; } = new List<Cart>();

    public virtual ICollection<CustomerAddress> CustomerAddress { get; } = new List<CustomerAddress>();

    public virtual ICollection<CustomerDocument> CustomerDocument { get; } = new List<CustomerDocument>();

    public virtual CustomerType CustomerType { get; set; } = null!;

    public virtual ICollection<Ticket> Ticket { get; } = new List<Ticket>();

    public virtual ICollection<UserAccount> UserAccount { get; } = new List<UserAccount>();
}