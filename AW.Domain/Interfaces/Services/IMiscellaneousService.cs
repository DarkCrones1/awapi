using AW.Common.Dtos.Response;

namespace AW.Domain.Interfaces.Services;

public interface IMiscellaneousService
{
    Task<IEnumerable<EnumValueResponseDto>> GetCartStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetCraftmanDocumentType();
    Task<IEnumerable<EnumValueResponseDto>> GetCraftmanStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetCraftStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetCustomerDocumentType();
    Task<IEnumerable<EnumValueResponseDto>> GetGender();
    Task<IEnumerable<EnumValueResponseDto>> GetPaymentStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetPropertyType();
    Task<IEnumerable<EnumValueResponseDto>> GetTicketStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetUserAccountType();
    
}