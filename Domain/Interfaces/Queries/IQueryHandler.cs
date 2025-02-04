namespace Domain.Interfaces.Queries;

public interface IQueryHandler<in TQuery, TResult> where TQuery :IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}

