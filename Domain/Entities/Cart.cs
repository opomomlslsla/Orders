using Domain.Entities.Common;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Cart : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<CartProduct> Products { get; set; } = new List<CartProduct>();
}