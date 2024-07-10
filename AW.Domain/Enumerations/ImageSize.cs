using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum ImageSize
{
    [Description("Pequeña")]
    Small = 1,
    [Description("Mediana")]
    Medium = 2,
    [Description("Grande")]
    Large = 3,
    [Description("4K")]
    FourK = 4,
}