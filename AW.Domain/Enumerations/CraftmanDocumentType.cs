using System.ComponentModel;

namespace AW.Domain.Enumerations;

public enum CraftmanDocumentType
{
    [Description("INE")]
    INE = 1,
    [Description("CURP")]
    CURP = 2,
    [Description("RFC")]
    RFC = 3,
    [Description("Cédula Profesional")]
    Cedula_Profesional = 4,
    [Description("Pasaporte")]
    Passport = 5,
    [Description("INAPAM")]
    INAPAM = 6,
    [Description("Licencia de conducir")]
    Licencia_Conducir = 7,
    [Description("Comprobante de domicilio")]
    Customer_Proof_Address = 8,
    [Description("Otro")]
    Otro = 10
}