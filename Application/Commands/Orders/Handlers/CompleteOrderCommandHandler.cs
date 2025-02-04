using Domain.Enums;
using Domain.Interfaces.Commands;
using Infrastructure;

namespace Application.Commands.Orders.Handlers;

public class CompleteOrderCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<CompleteOrderCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CompleteOrderCommand command)
    {
        var order = await unitOfWork.OrderRepository.FirstAsync(x => x.Id == command.OrderId);
        if(order == null)
            return new Result<string>("fail", false,"ho order has beeb found by given Id");
        order.Status = OrderStatus.Done;
        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("success",true, "Order has been closed");
    }
}