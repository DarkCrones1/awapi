namespace AW.Domain.Dto.Response;

public class TicketDetailResponseDto
{
    public int Id { get; set; }

    public Guid SerialId { get; set; }

    public int CartId { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public DateTime CloseTicket { get; set; }

    public CartDetailResponseDto Cart { get; } = new CartDetailResponseDto();
}