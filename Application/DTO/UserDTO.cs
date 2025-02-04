namespace Application.DTO;

public class UserDTO
{
    public Guid Id { get; set; }
    public string Role { get; set; } = null!;
    public CustomerDTO? Customer { get; set; }
}
