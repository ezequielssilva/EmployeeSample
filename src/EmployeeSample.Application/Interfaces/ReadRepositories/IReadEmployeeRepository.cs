using EmployeeSample.Application.Features.Employees.Queries;
using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Interfaces.ReadRepositories;

public interface IReadEmployeeRepository : IReadRepository<Employee>
{
    Task<bool> CheckDocumentExists(string document);
    Task<bool> CheckDocumentExists(string employeeId, string document);
    Task<Employee?> GetByDocument(string document);

    Task<IEnumerable<EmployeeDto>> GetAll(int page, int pageSize = 10);
}
