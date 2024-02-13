using Microsoft.Extensions.Configuration;

using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
// using AW.Domain.Interfaces.Repositories;

// using Res.Domain.Interfaces.Repositories;
using AW.Infrastructure.Data;

namespace AW.Infrastructure.Repositories;

public class UnirOfWork : IUnitOfWork
{
    protected readonly AWDbContext _dbContext;

    private readonly IConfiguration _configuration;

    protected ICrudRepository<Address> _addressRepository;

    protected ICrudRepository<Administrator> _administratorRepository;

    protected ICrudRepository<Cart> _cartRepository;

    protected ICatalogBaseRepository<Category> _categoryRepository;

    protected ICrudRepository<City> _cityRepository;

    protected ICrudRepository<Country> _countryRepository;

    protected ICatalogBaseRepository<Craft> _craftRepository;

    protected ICrudRepository<Craftman> _craftmanRepository;

    protected ICatalogBaseRepository<Culture> _cultureRepository;

    protected ICrudRepository<Customer> _customerRepository;

    protected ICrudRepository<CustomerAddress> _customerAddressRepository;

    protected ICatalogBaseRepository<CustomerType> _customerTypeRepository;

    protected ICatalogBaseRepository<Rol> _rolRepository;

    protected ICrudRepository<Sale> _saleRepository;

    protected ICatalogBaseRepository<TechniqueType> _techniqueTypeRepository;

    protected ICrudRepository<Ticket> _ticketRepository;

    protected ICrudRepository<UserAccount> _userAccountRepository;

    protected IRetrieveRepository<ActiveUserAccountAdministrator> _activeUserAccountAdministratorRepository;

    protected IRetrieveRepository<ActiveUserAccountCraftman> _activeUserAccountCraftmanRepository;

    protected IRetrieveRepository<ActiveUserAccountCustomer> _activeUserAccountCustomerRepository;

    private bool disposed;

    public UnirOfWork(AWDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;

        this._configuration = configuration;

        disposed = false;

        _addressRepository = new CrudRepository<Address>(_dbContext);

        _administratorRepository = new CrudRepository<Administrator>(_dbContext);

        _cartRepository = new CrudRepository<Cart>(_dbContext);

        _categoryRepository = new CatalogBaseRepository<Category>(_dbContext);

        _cityRepository = new CrudRepository<City>(_dbContext);

        _countryRepository = new CrudRepository<Country>(_dbContext);

        _craftRepository = new CatalogBaseRepository<Craft>(_dbContext);

        _craftmanRepository = new CrudRepository<Craftman>(_dbContext);

        _cultureRepository = new CatalogBaseRepository<Culture>(_dbContext);

        _customerRepository = new CrudRepository<Customer>(_dbContext);

        _customerAddressRepository = new CrudRepository<CustomerAddress>(_dbContext);

        _customerTypeRepository = new CatalogBaseRepository<CustomerType>(_dbContext);

        _rolRepository = new CatalogBaseRepository<Rol>(_dbContext);

        _saleRepository = new CrudRepository<Sale>(_dbContext);

        _techniqueTypeRepository = new CatalogBaseRepository<TechniqueType>(_dbContext);

        _ticketRepository = new CrudRepository<Ticket>(_dbContext);

        _userAccountRepository = new CrudRepository<UserAccount>(_dbContext);

        _activeUserAccountAdministratorRepository = new RetrieveRepository<ActiveUserAccountAdministrator>(_dbContext);

        _activeUserAccountCraftmanRepository = new RetrieveRepository<ActiveUserAccountCraftman>(_dbContext);

        _activeUserAccountCustomerRepository = new RetrieveRepository<ActiveUserAccountCustomer>(_dbContext);
    }

    public ICrudRepository<Address> AddressRepository => _addressRepository;

    public ICrudRepository<Administrator> AdministratorRepository => _administratorRepository;

    public ICrudRepository<Cart> CartRepository => _cartRepository;

    public ICatalogBaseRepository<Category> CategoryRepository => _categoryRepository;

    public ICrudRepository<City> CityRepository => _cityRepository;

    public ICrudRepository<Country> CountryRepository => _countryRepository;

    public ICatalogBaseRepository<Craft> CraftRepository => _craftRepository;

    public ICrudRepository<Craftman> CraftmanRepository => _craftmanRepository;

    public ICatalogBaseRepository<Culture> CultureRepository => _cultureRepository;

    public ICrudRepository<Customer> CustomerRepository => _customerRepository;

    public ICrudRepository<CustomerAddress> CustomerAddressRepository => _customerAddressRepository;

    public ICatalogBaseRepository<CustomerType> CustomerTypeRepository => _customerTypeRepository;

    public ICatalogBaseRepository<Rol> RolRepository => _rolRepository;

    public ICrudRepository<Sale> SaleRepository => _saleRepository;

    public ICatalogBaseRepository<TechniqueType> TechniqueTypeRepository => _techniqueTypeRepository;

    public ICrudRepository<Ticket> TicketRepository => _ticketRepository;

    public ICrudRepository<UserAccount> UserAccountRepository => _userAccountRepository;

    public IRetrieveRepository<ActiveUserAccountAdministrator> ActiveUserAccountAdministratorRepository => _activeUserAccountAdministratorRepository;

    public IRetrieveRepository<ActiveUserAccountCraftman> ActiveUserAccountCraftmanRepository => _activeUserAccountCraftmanRepository;

    public IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository => _activeUserAccountCustomerRepository;

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