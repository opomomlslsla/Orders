using Application.DTO;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Commands;
using Domain.ValueObjects;
using Infrastructure;
using Mapster;

namespace Application.Commands.Orders.Handlers;

public class AddOrderCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<AddOrderCommand, Result<OrderDTO>>
{
    public async Task<Result<OrderDTO>> HandleAsync(AddOrderCommand command)
    {
        var custumer = await unitOfWork.CustomerRepository.FirstAsync(x => x.Id == command.CustomerId);
        if (custumer == null)
            return new Result<OrderDTO>(null, false, "User not found, can not add no one's order right?", 404);
        var cart = await unitOfWork.CartRepository.FirstAsync(x => x.CustomerId == custumer.Id);
        
        var orderItems = cart!.Products.Select(p => new OrderItem()
        {
            ProductId = p.ProductId,
            ItemPrice = p.Price,
            ItemCount = p.Quatity
        }).ToList();
        var order = new Order()
        {
            CustomerId = custumer.Id,
            OrderDate = DateTime.Now,
            Status = OrderStatus.New,
            OrderItems = orderItems
        };
        
        await unitOfWork.OrderRepository.AddAsync(order);
        cart.Products = new List<CartProduct>();
        unitOfWork.CartRepository.Update(cart);
        await unitOfWork.SaveChangesAsync();
        return new Result<OrderDTO>(order.Adapt<OrderDTO>(), true, "order added");
    }
}