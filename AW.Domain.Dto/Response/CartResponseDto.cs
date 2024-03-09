namespace AW.Domain.Dto.Response;

public class CartResponseDto
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public decimal Total { get; set; }
}