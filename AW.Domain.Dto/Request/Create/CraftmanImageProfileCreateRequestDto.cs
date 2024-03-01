using Microsoft.AspNetCore.Http;

namespace AW.Domain.Dto.Request.Create;

public class CraftmanImageProfileCreateRequestDto
{
    public int CraftmanId { get; set; }

    public IFormFile? File { get; set; }
}