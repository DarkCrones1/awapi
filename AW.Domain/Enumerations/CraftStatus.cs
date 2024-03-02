using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum CraftStatus
{
    [Description("En Existencia")]
    Stock = 1,
    [Description("Agotado")]
    SoldOut = 2,
    [Description("Descontinuado")]
    Discontinues = 3

}