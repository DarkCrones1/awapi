using System.Linq.Expressions;
using AW.Common.Entities;
using AW.Domain.Dto.QueryFilters;
using AW.Domain.Entities;
using AW.Domain.Interfaces;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class CustomerService : CrudService<Customer>, ICustomerService
{
    public CustomerService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public async Task<PagedList<Customer>> GetPaged(CustomerQueryFilter filter)
    {
        var result = await _unitOfWork.CustomerRepository.GetPaged(filter);
        var pagedItems = PagedList<Customer>.Create(result, filter.PageNumber, filter.PageSize);
        return pagedItems;
    }

    public override async Task Update(Customer entity)
    {
        var oldEntity = await _unitOfWork.CustomerRepository.GetById(entity.Id);
        var customerAddress = oldEntity.CustomerAddress.FirstOrDefault()!;
        var adress = customerAddress.Address ?? new Address();

        var newCustomerAddress = entity.CustomerAddress.FirstOrDefault()!;
        var newAddress = newCustomerAddress.Address ?? new Address();

        customerAddress.CustomerId = customerAddress.CustomerId;
        customerAddress.AddressId = customerAddress.AddressId;
        customerAddress.IsDefault = true;
        customerAddress.Status = 1;
        customerAddress.RegisterDate = DateTime.Now;
        customerAddress.Id = customerAddress.Id;

        adress.Id = adress.Id;
        adress.Address1 = newAddress.Address1;
        adress.Address2 = newAddress.Address2;
        adress.Street = newAddress.Street;
        adress.ExternalNumber = newAddress.ExternalNumber;
        adress.InternalNumber = newAddress.InternalNumber;
        adress.ZipCode = newAddress.ZipCode;
        adress.CityId = newAddress.CityId;

        _unitOfWork.CustomerAddressRepository.Update(customerAddress);
        _unitOfWork.AddressRepository.Update(adress);

        entity.Code = oldEntity.Code;
        entity.ProfilePictureUrl = oldEntity.ProfilePictureUrl;
        entity.CustomerTypeId = oldEntity.CustomerTypeId;
        await base.Update(entity);
        await _unitOfWork.SaveChangesAsync();
    }
}