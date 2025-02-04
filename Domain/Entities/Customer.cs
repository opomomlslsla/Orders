using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entities.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Code { get; set; } = null!;
    [Required]
    public string? Address { get; set; }
    public decimal? Discount { get; set; }
    }