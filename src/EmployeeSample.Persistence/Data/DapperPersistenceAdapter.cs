using System.Data;
using Dapper;
using EmployeeSample.Application.Interfaces.Data;

namespace EmployeeSample.Persistence.Data;

public class DapperPersistenceAdapter : IPersistenceAdapter, IDisposable
{
    private readonly IDbConnection _connection;
    private IDbTransaction? _transaction;

    public DapperPersistenceAdapter(IDbConnection connection) =>
        _connection = connection;

    public IDbConnection GetConnection()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        return _connection;
    }

    public async Task<int> ExecuteAsync(string query, object parameters) =>
        await _connection.ExecuteAsync(query, parameters, _transaction);

    public async Task<T> FirstOrDefaultAsync<T>(string query, object? parameters = null) where T : class =>
        await _connection.QueryFirstOrDefaultAsync<T>(query, parameters);

    public async Task<bool> QueryAnyAsync(string query, object? parameters = null) =>
        (await _connection.QueryFirstOrDefaultAsync<int>(query, parameters)) > 0;

    public async Task<IEnumerable<T>> QueryAsync<T>(string query, object? parameters = null) where T : class =>
        await _connection.QueryAsync<T>(query, parameters);

    public void BeginTransaction()
    {
        if (_connection.State != ConnectionState.Open)
            _connection.Open();

        _transaction = _connection.BeginTransaction();
    }

    public void CommitTransaction()
    {
        if (_transaction == null)
            throw new NullReferenceException("Transaction not started");

        _transaction.Commit();
    }

    public void RollbackTransaction()
    {
        if (_transaction == null)
            throw new NullReferenceException("Transaction not started");

        _transaction.Rollback();
    }

    public void Dispose()
    {
        _connection.Dispose();
        _transaction?.Dispose();
    }
}
