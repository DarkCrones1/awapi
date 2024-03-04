using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum PropertyType
{
    [Description("Artesania")]
    CraftProperty = 1,
    [Description("Tipo de tecnica")]
    TechniqueProperty = 2
}