using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum PaymentStatus
{
    [Description("Pendiente")]
    Pendding = 1,
    [Description("Pagado")]
    Payment = 2,
    [Description("Cancelado")]
    Canceled = 5
}