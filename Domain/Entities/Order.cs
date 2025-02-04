using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Order : BaseEntity
{
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public DateTime OrderDate { get; set; }
    public DateTime DeliveryDate { get; set; }
    public long OrderNumber { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}