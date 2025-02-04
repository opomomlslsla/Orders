using Domain.Entities;

namespace Domain.ValueObjects;

public class CartProduct
{
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quatity { get; set; }
    public decimal Price { get; set; }
}