namespace AW.Api.Helpers;

public class SettingConfigurationFile
{
    private static SettingConfigurationFile? _instance;
    private readonly IConfiguration _configuration;

    public static SettingConfigurationFile Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new InvalidOperationException("SettingsConfigurationFile has not been initialized");
            }
            return _instance;
        }
    }

    private SettingConfigurationFile(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static void Initialize(IConfiguration configuration)
    {
        if (_instance != null)
        {
            return;
        }
        _instance = new SettingConfigurationFile(configuration);
    }

    public int AdministratorAccountTypeId => int.Parse(_configuration.GetValue<string>("DefaultValues:administratorAccountTypeId")!);
    public int CraftmanAccountTypeId => int.Parse(_configuration.GetValue<string>("DefaultValues:craftmanAccountTypeId")!);
    public int CustomerAccountTypeId => int.Parse(_configuration.GetValue<string>("DefaultValues:customerAccountTypeId")!);

    public string UrlLocalImageCraft => _configuration.GetValue<string>("DefaultValues:craftImageLocalStorageBaseUrl") ?? string.Empty;
    public string UrlLocalImageProfile => _configuration.GetValue<string>("DefaultValues:ImageProfileLocalStorageBaseUrl") ?? string.Empty;
    public string UrlLocalImageCategory => _configuration.GetValue<string>("DefaultValues:categoryImageLocalStorageBaseUrl") ?? string.Empty;
    public string UrlLocalImageCustomerDocument => _configuration.GetValue<string>("DefaultValues:customerDocuments") ?? string.Empty;
    public string UrlLocalImageCraftmanDocument => _configuration.GetValue<string>("DefaultValues:craftmanDocuments") ?? string.Empty;
}