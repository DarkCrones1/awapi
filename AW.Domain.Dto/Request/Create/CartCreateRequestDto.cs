namespace AW.Domain.Dto.Request.Create;

public class CartCreateRequestDto
{
    public int CustomerId { get; set; }

    public int[]? CraftIds { get; set; }
}