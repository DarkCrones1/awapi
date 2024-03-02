using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
// using AW.Domain.Interfaces.Repositories;

namespace AW.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Address> AddressRepository { get; }

    ICrudRepository<Administrator> AdministratorRepository { get; }

    ICrudRepository<Cart> CartRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    ICatalogBaseRepository<Property> PropertyRepository { get; }

    ICrudRepository<City> CityRepository { get; }

    ICrudRepository<Country> CountryRepository { get; }

    ICatalogBaseRepository<Craft> CraftRepository { get; }

    ICrudRepository<CraftProperty> CraftPropertyRepository { get; }

    ICraftmantRepository CraftmanRepository { get; }

    ICatalogBaseRepository<Culture> CultureRepository { get; }

    ICrudRepository<Customer> CustomerRepository { get; }

    ICrudRepository<CustomerAddress> CustomerAddressRepository { get; }

    ICatalogBaseRepository<CustomerType> CustomerTypeRepository { get; }

    ICatalogBaseRepository<Rol> RolRepository { get; }

    ICrudRepository<Sale> SaleRepository { get; }

    ITechniqueTypeRepository TechniqueTypeRepository { get; }

    ICrudRepository<TechniqueTypeProperty> TechniqueTypePropertyRepository { get; }

    ICrudRepository<Ticket> TicketRepository { get; }

    IUserAccountRepository UserAccountRepository { get; }

    IRetrieveRepository<ActiveUserAccountAdministrator> ActiveUserAccountAdministratorRepository { get; }

    IRetrieveRepository<ActiveUserAccountCraftman> ActiveUserAccountCraftmanRepository { get; }

    IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository { get; }

    IAzureBlobStorageRepository AzureBlobStorageRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}