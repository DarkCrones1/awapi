namespace AW.Domain.Dto.Response;

public class TicketResponseDto
{
    public int Id { get; set; }

    public Guid SerialId { get; set; }

    public int BranchStoreId { get; set; }

    public string? BranchStoreName { get; set; }

    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public string? CustomerFullName { get; set; }

    public short Status { get; set; }

    public string? StatusName { get; set; }

    public DateTime CloseTicket { get; set; }
}