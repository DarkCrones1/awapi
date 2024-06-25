using System.ComponentModel;

namespace AW.Common.Enumerations;

public enum LocalContainer
{
    [Description("Imagenes de artesanias")]
    Image_Craft = 1,
    [Description("Imagenes de perfil artesano")]
    Image_Profile_Craftman = 2,
    [Description("Imagen de perfil cliente")]
    Image_Profile_Customer = 3,
    [Description("Imagen de categorias")]
    Image_Category = 4,
    [Description("Otros documentos de cliente")]
    Customer_Other_Documents = 5,
    [Description("Otros documentos de artesanos")]
    Craftman_Other_Documents = 6,
}