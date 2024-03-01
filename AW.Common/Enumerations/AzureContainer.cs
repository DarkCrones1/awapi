using System.ComponentModel;

namespace AW.Common.Enumerations;

public enum AzureContainer
{
    [Description("Identificaciones de cliente")]
    Customer_Identification = 1,
    [Description("Comprobantes de domicilio")]
    Customer_Proof_Address = 2,
    [Description("Imagenes de perfil")]
    Image_Profile = 3,
    [Description("Otros documentos de cliente")]
    Customer_Other_Documents = 4,
    [Description("Documentos de Artesanos")]
    Craftman_Documents = 5,
}
