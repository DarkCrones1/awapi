namespace AW.Domain.Dto.Request.Create;

public class CartCreateRequestDto
{
    private IEnumerable<CartCraftCreateRequestDto> _cartCrafts;

    public CartCreateRequestDto()
    {
        _cartCrafts = new List<CartCraftCreateRequestDto>();
    }

    public int CustomerId { get; set; }

    public IEnumerable<CartCraftCreateRequestDto> CartCrafts { get => _cartCrafts; set => _cartCrafts = value ?? new List<CartCraftCreateRequestDto>(); }
}