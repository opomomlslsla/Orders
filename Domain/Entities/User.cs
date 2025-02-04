using System.ComponentModel.DataAnnotations;
using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : BaseEntity
{
    [Required]
    public string Login { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
    public Role Role { get; set; }
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
}