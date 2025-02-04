using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Code { get; set; }
    [MaxLength(30)]
    public string? Category { get; set; }
}