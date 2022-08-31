using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Persistence.WriteRepositories;

public abstract class WriteBaseRepository
{
    protected readonly IPersistenceAdapter _adapter;

    public WriteBaseRepository(IPersistenceAdapter adapter) =>
        _adapter = adapter;

    public void BeginTransaction() => _adapter.BeginTransaction();

    public void CommitTransaction() => _adapter.CommitTransaction();

    public void RollbackTransaction() => _adapter.RollbackTransaction();
}
