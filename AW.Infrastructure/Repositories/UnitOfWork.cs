using Microsoft.Extensions.Configuration;

using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AW.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected readonly AWDbContext _dbContext;

    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _env;

    private readonly IHttpContextAccessor _httpContextAccessor;

    protected ICrudRepository<Address> _addressRepository;

    protected ICrudRepository<Administrator> _administratorRepository;

    protected ICartRepository _cartRepository;

    protected ICategoryRepository _categoryRepository;

    protected ICatalogBaseRepository<Property> _propertyRepository;

    protected ICrudRepository<City> _cityRepository;

    protected ICrudRepository<Country> _countryRepository;

    protected ICraftRepository _craftRepository;

    protected ICrudRepository<CraftProperty> _craftPropertyRepository;

    protected ICraftmantRepository _craftmanRepository;

    protected ICultureRepository _cultureRepository;

    protected ICustomerRepository _customerRepository;

    protected ICrudRepository<CustomerAddress> _customerAddressRepository;

    protected ICatalogBaseRepository<CustomerType> _customerTypeRepository;

    protected ICrudRepository<Payment> _paymentRepository;

    protected ICatalogBaseRepository<Rol> _rolRepository;

    protected ICrudRepository<Sale> _saleRepository;

    protected ITechniqueTypeRepository _techniqueTypeRepository;

    protected ICrudRepository<TechniqueTypeProperty> _techniqueTypePropertyRepository;

    protected ICrudRepository<Ticket> _ticketRepository;

    protected IUserAccountRepository _userAccountRepository;

    protected ICrudRepository<UserDataInfo> _userDataInfoRepository;

    protected IRetrieveRepository<ActiveUserAccount> _activeUserAccountRepository;

    protected IRetrieveRepository<ActiveUserAccountAdministrator> _activeUserAccountAdministratorRepository;

    protected IRetrieveRepository<ActiveUserAccountCraftman> _activeUserAccountCraftmanRepository;

    protected IRetrieveRepository<ActiveUserAccountCustomer> _activeUserAccountCustomerRepository;

    protected IAzureBlobStorageRepository _azureBlobStorageRepository;

    protected ILocalStorageRepository _localStorageRepository;

    private bool disposed;

    public UnitOfWork(AWDbContext dbContext, IConfiguration configuration, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        this._env = env;

        this._httpContextAccessor = httpContextAccessor;

        disposed = false;

        _addressRepository = new CrudRepository<Address>(_dbContext);

        _administratorRepository = new CrudRepository<Administrator>(_dbContext);

        _cartRepository = new CartRepository(_dbContext);

        _categoryRepository = new CategoryRepository(_dbContext);

        _propertyRepository = new CatalogBaseRepository<Property>(_dbContext);

        _cityRepository = new CrudRepository<City>(_dbContext);

        _countryRepository = new CrudRepository<Country>(_dbContext);

        _craftRepository = new CraftRepository(_dbContext);

        _craftmanRepository = new CraftmanRepository(_dbContext);

        _craftPropertyRepository = new CrudRepository<CraftProperty>(_dbContext);

        _cultureRepository = new CultureRepository(_dbContext);

        _customerRepository = new CustomerRepository(_dbContext);

        _customerAddressRepository = new CrudRepository<CustomerAddress>(_dbContext);

        _customerTypeRepository = new CatalogBaseRepository<CustomerType>(_dbContext);

        _paymentRepository = new CrudRepository<Payment>(_dbContext);

        _rolRepository = new CatalogBaseRepository<Rol>(_dbContext);

        _saleRepository = new CrudRepository<Sale>(_dbContext);

        _techniqueTypeRepository = new TechniqueTypeRepository(_dbContext);

        _techniqueTypePropertyRepository = new CrudRepository<TechniqueTypeProperty>(_dbContext);

        _ticketRepository = new CrudRepository<Ticket>(_dbContext);

        _userAccountRepository = new UserAccountRepository(_dbContext);

        _userDataInfoRepository = new CrudRepository<UserDataInfo>(_dbContext);

        _activeUserAccountRepository = new RetrieveRepository<ActiveUserAccount>(_dbContext);

        _activeUserAccountAdministratorRepository = new RetrieveRepository<ActiveUserAccountAdministrator>(_dbContext);

        _activeUserAccountCraftmanRepository = new RetrieveRepository<ActiveUserAccountCraftman>(_dbContext);

        _activeUserAccountCustomerRepository = new RetrieveRepository<ActiveUserAccountCustomer>(_dbContext);

        _azureBlobStorageRepository = new AzureBlobStorageRepository(_configuration);

        _localStorageRepository = new LocalStorageRepository(_configuration, _env, _httpContextAccessor);
    }

    public ICrudRepository<Address> AddressRepository => _addressRepository;

    public ICrudRepository<Administrator> AdministratorRepository => _administratorRepository;

    public ICartRepository CartRepository => _cartRepository;

    public ICategoryRepository CategoryRepository => _categoryRepository;

    public ICatalogBaseRepository<Property> PropertyRepository => _propertyRepository;

    public ICrudRepository<City> CityRepository => _cityRepository;

    public ICrudRepository<Country> CountryRepository => _countryRepository;

    public ICraftRepository CraftRepository => _craftRepository;

    public ICrudRepository<CraftProperty> CraftPropertyRepository => _craftPropertyRepository;

    public ICraftmantRepository CraftmanRepository => _craftmanRepository;

    public ICultureRepository CultureRepository => _cultureRepository;

    public ICustomerRepository CustomerRepository => _customerRepository;

    public ICrudRepository<CustomerAddress> CustomerAddressRepository => _customerAddressRepository;

    public ICatalogBaseRepository<CustomerType> CustomerTypeRepository => _customerTypeRepository;

    public ICrudRepository<Payment> PaymentRepository => _paymentRepository;

    public ICatalogBaseRepository<Rol> RolRepository => _rolRepository;

    public ICrudRepository<Sale> SaleRepository => _saleRepository;

    public ITechniqueTypeRepository TechniqueTypeRepository => _techniqueTypeRepository;

    public ICrudRepository<TechniqueTypeProperty> TechniqueTypePropertyRepository => _techniqueTypePropertyRepository;

    public ICrudRepository<Ticket> TicketRepository => _ticketRepository;

    public IUserAccountRepository UserAccountRepository => _userAccountRepository;

    public ICrudRepository<UserDataInfo> UserDataInfoRepository => _userDataInfoRepository;

    public IRetrieveRepository<ActiveUserAccount> ActiveUserAccountRepository => _activeUserAccountRepository;

    public IRetrieveRepository<ActiveUserAccountAdministrator> ActiveUserAccountAdministratorRepository => _activeUserAccountAdministratorRepository;

    public IRetrieveRepository<ActiveUserAccountCraftman> ActiveUserAccountCraftmanRepository => _activeUserAccountCraftmanRepository;

    public IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository => _activeUserAccountCustomerRepository;

    public IAzureBlobStorageRepository AzureBlobStorageRepository => _azureBlobStorageRepository;

    public ILocalStorageRepository LocalStorageRepository => _localStorageRepository;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
            if (disposing)
                _dbContext.Dispose();

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}