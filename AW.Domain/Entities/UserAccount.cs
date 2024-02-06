using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class UserAccount : BaseRemovablePaginationEntity
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsAuthorized { get; set; }

    public short AccountType { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<Customer> Customer { get; } = new List<Customer>();

    public virtual ICollection<Craftman> Craftman { get; } = new List<Craftman>();

    public virtual ICollection<Administrator> Administrator { get; } = new List<Administrator>();

    public virtual ICollection<Rol> Rol { get; } = new List<Rol>();
}