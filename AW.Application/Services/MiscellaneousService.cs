using AW.Common.Dtos.Response;
using AW.Common.Helpers;
using AW.Domain.Enumerations;
using AW.Domain.Interfaces.Services;

namespace AW.Application.Services;

public class MiscellaneousService : IMiscellaneousService
{
    public async Task<IEnumerable<EnumValueResponseDto>> GetCartStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CartStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetCraftmanDocumentType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CraftmanDocumentType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetCraftmanStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CraftmanStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetCraftStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CraftStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetCustomerDocumentType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<CustomerDocumentType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetGender()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<Gender>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetPaymentStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<PaymentStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetPropertyType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<PropertyType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetTicketStatus()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<TicketStatus>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }

    public async Task<IEnumerable<EnumValueResponseDto>> GetUserAccountType()
    {
        var lstItems = new List<EnumValueResponseDto>();

        lstItems = EnumHelper.GetEnumItems<UserAccountType>().ToList();

        await Task.CompletedTask;

        return lstItems ?? new List<EnumValueResponseDto>();
    }
}