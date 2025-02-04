using Domain.Interfaces.Commands;
using Domain.Interfaces.Queries;

namespace Domain.Interfaces;


public interface IDispatcher
{
    Task<TResult> DispatchCommandAsync<TCommand, TResult>(TCommand command) where TCommand : ICommand;
    Task<TResult> DispatchQueryAsync<TQuery, TResult>(TQuery command) where TQuery : IQuery;
}