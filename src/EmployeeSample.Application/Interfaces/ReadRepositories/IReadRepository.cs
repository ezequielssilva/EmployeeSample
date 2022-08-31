using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Interfaces.ReadRepositories;

public interface IReadRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<TEntity?> GetById(string id);
    Task<bool> CheckExistsById(string id);
}
