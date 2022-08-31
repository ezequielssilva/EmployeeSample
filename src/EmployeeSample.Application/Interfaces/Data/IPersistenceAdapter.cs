using System.Data;

namespace EmployeeSample.Application.Interfaces.Data;

public interface IPersistenceAdapter
{
    Task<int> ExecuteAsync(string query, object parameters);
    Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null) where T : class;
    Task<bool> QueryAnyAsync(string query, object? parameters = null);
    Task<T> FirstOrDefaultAsync<T>(string query, object? parameters = null) where T : class;

    IDbConnection GetConnection();

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
