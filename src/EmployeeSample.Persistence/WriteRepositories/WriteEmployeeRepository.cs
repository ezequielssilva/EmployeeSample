using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Persistence.Queries;

namespace EmployeeSample.Persistence.WriteRepositories;

public class WriteEmployeeRepository : WriteBaseRepository, IWriteEmployeeRepository
{
    public WriteEmployeeRepository(IPersistenceAdapter adapter) : base(adapter) { }

    public Task Insert(Employee entity)
    {
        return _adapter.ExecuteAsync(
            EmployeeQuery.QueryInsert,
            new
            {
                Id = entity.Id,
                Document = entity.Document.ToString(),
                FullName = entity.Name.FullName,
                SocialName = entity.Name.SocialName,
                Sex = (char)entity.Sex,
                MaritalStatus = (int)entity.MaritalStatus,
                EducationLevel = (int)entity.EducationLevel,
                BirthDate = entity.BirthDate.Date,
                Phone = entity.Phone.ToString(),
                Email = entity.Email.ToString(),
                CreatedAt = entity.CreatedAt
            }
        );
    }

    public Task Update(Employee entity) =>
        _adapter.ExecuteAsync(
            EmployeeQuery.QueryUpdateById,
            new
            {
                Id = entity.Id.ToString(),
                Document = entity.Document.ToString(),
                FullName = entity.Name.FullName,
                SocialName = entity.Name.SocialName,
                Sex = (char)entity.Sex,
                MaritalStatus = (int)entity.MaritalStatus,
                EducationLevel = (int)entity.EducationLevel,
                BirthDate = entity.BirthDate.Date,
                Phone = entity.Phone.ToString(),
                Email = entity.Email.ToString(),
                UpdatedAt = entity.UpdatedAt
            }
        );

    public Task Delete(string id) =>
        _adapter.ExecuteAsync(
            EmployeeQuery.QueryDeleteById,
            new
            {
                Id = id
            }
        );

    public Task Delete(Employee entity) =>
        this.Delete(entity.Id.ToString());
}
