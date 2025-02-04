using Domain.Interfaces;
using Domain.Interfaces.Commands;
using Domain.Interfaces.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commands;

public class Dispatcher(IServiceProvider serviceProvider) : IDispatcher
{
    public async Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand
    {
        var handler = serviceProvider.GetService<ICommandHandler<TCommand, TResult>>();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler found for command type {typeof(TCommand).Name}");
        }
        return await handler.HandleAsync(command);
    }

    public async Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery query) where TQuery : IQuery
    {
        var handler = serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();
        if (handler == null)
        {
            throw new InvalidOperationException($"No handler found for command type {typeof(TQuery).Name}");
        }
        return await handler.HandleAsync(query);
    }
}

