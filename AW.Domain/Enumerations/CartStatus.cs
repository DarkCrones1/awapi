using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum CartStatus
{
    [Description("Ordenado")]
    Arrange = 1,
    [Description("Pendiente")]
    Pendding = 2,
    [Description("Terminado")]
    Finished = 3,
    [Description("Cerrado")]
    Closed = 4,
    [Description("Cancelado")]
    Canceled = 10
}