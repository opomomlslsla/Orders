using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using Infrastructure.Data;

namespace Infrastructure;

public class UnitOfWork(
    Context context,
    IRepository<User> userRepository,
    IRepository<Order> orderRepository,
    IRepository<Product> productRepository,
    IRepository<Customer> customerRepository,
    IRepository<Cart> cartRepository)
{
    public IRepository<User> UserRepository { get; } = userRepository;
    public IRepository<Order> OrderRepository { get; } = orderRepository;
    public IRepository<Product> ProductRepository { get; } = productRepository;
    public IRepository<Customer> CustomerRepository { get; } = customerRepository;
    public IRepository<Cart> CartRepository { get; } = cartRepository;
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}