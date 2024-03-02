using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum PropertyType
{
    [Description("Artesania")]
    CraftProperty = 1,
    [Description("Tecnica Artesanal")]
    TechniqueProperty = 2
}