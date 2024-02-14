using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum UserAccountType
{
    [Description("Administrador")]
    Admin = 1,
    [Description("Artesano")]
    Craftman = 2,
    [Description("Cliente")]
    Customer = 3
}