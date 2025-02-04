namespace Application.DTO;

public class CartProductDTO
{
    public Guid ProductId { get; set; }
    public ProductDTO Product { get; set; } = null!;
    public int Quatity { get; set; }
    public decimal Price { get; set; }
}