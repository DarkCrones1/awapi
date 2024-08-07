using AW.Common.Interfaces.Repositories;
using AW.Domain.Entities;
using AW.Domain.Interfaces.Repositories;
// using AW.Domain.Interfaces.Repositories;

namespace AW.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICrudRepository<Address> AddressRepository { get; }

    ICrudRepository<Administrator> AdministratorRepository { get; }

    ICartRepository CartRepository { get; }

    ICrudRepository<CartCraft> CartCraftRepository { get; }

    ICategoryRepository CategoryRepository { get; }

    ICatalogBaseRepository<Property> PropertyRepository { get; }

    ICrudRepository<City> CityRepository { get; }

    ICrudRepository<Country> CountryRepository { get; }

    ICraftRepository CraftRepository { get; }

    ICrudRepository<CraftProperty> CraftPropertyRepository { get; }

    ICrudRepository<CraftPictureUrl> CraftPictureUrlRepository { get; }

    ICraftmantRepository CraftmanRepository { get; }

    ICultureRepository CultureRepository { get; }

    ICustomerRepository CustomerRepository { get; }

    ICrudRepository<CustomerAddress> CustomerAddressRepository { get; }

    ICatalogBaseRepository<CustomerType> CustomerTypeRepository { get; }

    ICrudRepository<Payment> PaymentRepository { get; }

    ICatalogBaseRepository<Rol> RolRepository { get; }

    ICrudRepository<Sale> SaleRepository { get; }

    ITechniqueTypeRepository TechniqueTypeRepository { get; }

    ICrudRepository<TechniqueTypeProperty> TechniqueTypePropertyRepository { get; }

    ITicketRepository TicketRepository { get; }

    IUserAccountRepository UserAccountRepository { get; }

    IRetrieveRepository<ActiveUserAccountAdministrator> ActiveUserAccountAdministratorRepository { get; }

    IRetrieveRepository<ActiveUserAccountCraftman> ActiveUserAccountCraftmanRepository { get; }

    IRetrieveRepository<ActiveUserAccountCustomer> ActiveUserAccountCustomerRepository { get; }

    IAzureBlobStorageRepository AzureBlobStorageRepository { get; }

    ILocalStorageRepository LocalStorageRepository { get; }

    void SaveChanges();

    Task SaveChangesAsync();
}