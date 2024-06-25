using Microsoft.AspNetCore.Http;

namespace AW.Domain.Dto.Request.Create;

public class ImageProfileCreateRequestDto
{
    public IFormFile File { get; set; } = null!;
}