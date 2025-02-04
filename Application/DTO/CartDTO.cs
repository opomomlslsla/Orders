namespace Application.DTO;

public class CartDTO
{
    public Guid CustomerId { get; set; }
    public List<CartProductDTO> Products { get; set; } = new List<CartProductDTO>();
}

