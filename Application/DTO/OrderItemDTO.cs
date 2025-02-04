namespace Application.DTO;

public class OrderItemDTO
{
    public ProductDTO Product { get; set; } = null!;
    public Guid OrderId { get; set; }
    public int ItemCount { get; set; }
    public decimal ItemPrice { get; set; }
}