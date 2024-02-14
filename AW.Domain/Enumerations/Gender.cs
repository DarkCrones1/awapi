using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum Gender
{
    [Description("Masculino")]
    Male = 1,

    [Description("Femenino")]
    Female = 2,

    [Description("No binario")]
    NoBinarie = 3,

    [Description("LGBTQ+")]
    LGBTQPlus = 4,

    [Description("Otro")]
    Other = 5,

    [Description("Prefiero no especificarlo")]
    DontSay = 6,
}