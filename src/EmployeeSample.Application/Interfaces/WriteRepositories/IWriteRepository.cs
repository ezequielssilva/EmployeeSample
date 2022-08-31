using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Interfaces.WriteRepositories;

public interface IWriteRepository<TEntity>
    where TEntity : BaseEntity
{
    Task Insert(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(TEntity entity);
    Task Delete(string id);

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}
