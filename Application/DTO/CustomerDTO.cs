namespace Application.DTO;

public class CustomerDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Address { get; set; }
    public decimal? Discount { get; set; }
}