using Domain.Enums;
using Domain.Interfaces.Commands;
using Infrastructure;

namespace Application.Commands.Orders.Handlers;

public class ConfirmOrderCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<ConfirmOrderCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ConfirmOrderCommand command)
    {
        var order = await unitOfWork.OrderRepository.FirstAsync(x => x.Id == command.OrderId);
        if(order == null)
            return new Result<string>("fail", false,"ho order has been found by given Id",404);
        order.Status = OrderStatus.InProgress;
        unitOfWork.OrderRepository.Update(order);
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("success",true, "Order has been confirmed");
    }
}