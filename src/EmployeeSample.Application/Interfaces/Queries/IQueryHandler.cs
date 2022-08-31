namespace EmployeeSample.Application.Interfaces.Queries;

public interface IQueryHandler<TQuery, TQueryResult>
    where TQuery : IQuery
    where TQueryResult : class
{
    Task<TQueryResult> Execute(TQuery query);
}
