namespace Application.DTO;

public class OrderDTO
{
    public Guid Id { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long OrderNumber { get; set; }
    public string Status { get; set; } = null!;
    public List<OrderItemDTO> OrderItems { get; set; } = new();
}
