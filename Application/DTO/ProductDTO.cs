namespace Application.DTO;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public string? Code { get; set; }
    public string? Category { get; set; }
}