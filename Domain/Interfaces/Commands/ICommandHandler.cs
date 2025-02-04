namespace Domain.Interfaces.Commands;

public interface ICommandHandler<in TCommand,TResult> where TCommand : ICommand
{
    Task<TResult> HandleAsync(TCommand command);
}
