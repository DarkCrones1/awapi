using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum CartStatus
{
    [Description("Ordenado")]
    Arrange = 1,
    [Description("Proceso")]
    Process = 2,
    [Description("Pendiente")]
    Pendding = 3,
    [Description("Terminado")]
    Finished = 4,
    [Description("Cerrado")]
    Closed = 5,
    [Description("Cancelado")]
    Canceled = 10
}