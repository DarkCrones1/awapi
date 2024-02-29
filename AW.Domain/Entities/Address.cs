using AW.Common.Entities;

namespace AW.Domain.Entities;

public partial class Address : BaseEntity
{

    public string FullAddress
    {
        get
        {
            var address = string.Empty;
            var internalNumber = string.IsNullOrEmpty(InternalNumber) ? string.Empty : $"Num. Int. {InternalNumber}";

            address = $"Calle: {Street} Num. Ext. {ExternalNumber} {internalNumber} C.P. {ZipCode} {Address1}";

            return address.Trim();
        }
    }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public string Street { get; set; } = null!;

    public string ExternalNumber { get; set; } = null!;

    public string? InternalNumber { get; set; }

    public string ZipCode { get; set; } = null!;

    public virtual ICollection<Craftman> Craftman { get; } = new List<Craftman>();

    public virtual ICollection<CustomerAddress> CustomerAddress { get; } = new List<CustomerAddress>();
}