using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum CraftmanStatus
{
    [Description("Pendiente")]
    Pendding = 1,

    [Description("Aprobado")]
    Aproved = 2,

    [Description("Rechazado")]
    Rejected = 3,
    
    [Description("Baja")]
    Downed = 10
}