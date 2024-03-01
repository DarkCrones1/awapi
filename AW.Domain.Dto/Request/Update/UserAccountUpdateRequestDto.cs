namespace AW.Domain.Dto.Request.Update;

public class UserAccountUpdateRequestDto
{
    public string? UserName { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;
}