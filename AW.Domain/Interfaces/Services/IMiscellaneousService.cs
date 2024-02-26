using AW.Common.Dtos.Response;

namespace AW.Domain.Interfaces.Services;

public interface IMiscellaneousService
{
    Task<IEnumerable<EnumValueResponseDto>> GetCartStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetCraftmanStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetGender();
    Task<IEnumerable<EnumValueResponseDto>> GetTicketStatus();
    Task<IEnumerable<EnumValueResponseDto>> GetUserAccountType();
    
}