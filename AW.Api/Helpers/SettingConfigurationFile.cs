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

    public string UrlBlobCustomerIdentification => _configuration.GetValue<string>("DefaultValues:customerIdentificationAzureStorageBaseURL") ?? string.Empty;
    public string UrlBlobCustomerProfAddress => _configuration.GetValue<string>("DefaultValues:customerProofAddressAzureStorageBaseURL") ?? string.Empty;
    public string UrlBlobCustomerProfile => _configuration.GetValue<string>("DefaultValues:imageProfileAzureStorageBaseURL") ?? string.Empty;
    public string UrlBlobCustomerDocuments => _configuration.GetValue<string>("DefaultValues:customerDocuments") ?? string.Empty;
    public string UrlBlobEmployeeDocuments => _configuration.GetValue<string>("DefaultValues:employeeDocuments") ?? string.Empty;
    public int SysAdminRol => int.Parse(_configuration.GetValue<string>("DefaultValues:sysAdminRol")!);
    public int AdministratorRolId => int.Parse(_configuration.GetValue<string>("DefaultValues:administratorRolId")!);
    public int CashierRolId => int.Parse(_configuration.GetValue<string>("DefaultValues:cashierRolId")!);
    public int AppraiserRolId => int.Parse(_configuration.GetValue<string>("DefaultValues:appraiserRolId")!);
    public int ManagerGeographicalZone => int.Parse(_configuration.GetValue<string>("DefaultValues:managerGeographicalZone")!);
    public int ManagerBranchStoreRolId => int.Parse(_configuration.GetValue<string>("DefaultValues:managerBranchStoreRolId")!);
}