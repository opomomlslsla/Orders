using Domain.Enums;
using Domain.Interfaces.Commands;
using Infrastructure;

namespace Application.Commands.Orders.Handlers;

public class DeleteOrderCommandHandler(UnitOfWork unitOfWork) : ICommandHandler<DeleteOrderCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(DeleteOrderCommand command)
    {
        var order = await unitOfWork.OrderRepository.FirstAsync(x => x.Id == command.OrderId);
        if(order == null)
            return new Result<string>("fail", false, "no order have been found by given Id");
        if(order.Status != OrderStatus.New)
            return new Result<string>("fail", false, "order has already been confirmed");
        unitOfWork.OrderRepository.Delete(order);
        await unitOfWork.SaveChangesAsync();
        return new Result<string>("success", true, "order has been deleted");
    }
}