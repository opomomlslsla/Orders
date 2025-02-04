using Domain.Entities;

namespace Domain.ValueObjects;

public class OrderItem
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
    public int ItemCount { get; set; }
    public decimal ItemPrice { get; set; }
}