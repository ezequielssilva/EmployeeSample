using Dapper;
using EmployeeSample.Application.Features.Employees.Queries;
using EmployeeSample.Application.Interfaces.Data;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Persistence.Queries;

namespace EmployeeSample.Persistence.ReadRepositories;

public class ReadEmployeeRepositoryPersistence : IReadEmployeeRepository
{
    private readonly IPersistenceAdapter _adapter;

    public ReadEmployeeRepositoryPersistence(IPersistenceAdapter adapter) =>
        _adapter = adapter;

    public Task<bool> CheckDocumentExists(string document) =>
        _adapter.QueryAnyAsync(
            EmployeeQuery.QueryCheckDocumentExists,
            new
            {
                Document = document
            }
        );

    public Task<bool> CheckDocumentExists(string employeeId, string document) =>
        _adapter.QueryAnyAsync(
            EmployeeQuery.QueryCheckDocumentExistsById,
            new
            {
                Document = document,
                Id = employeeId
            }
        );

    public Task<bool> CheckExistsById(string id) =>
        _adapter.QueryAnyAsync(
            EmployeeQuery.CheckExistsById,
            new
            {
                Id = id
            }
        );

    private static (Type[] types, Func<object[], Employee?> map, string splitOn) MapToEntity()
    {
        return (
            types: new[] {
                typeof(Employee),
                typeof(string),     // Document
                typeof(string),     // FullName
                typeof(string),     // SocialName
                typeof(string),     // Sex
                typeof(int),        // MaritalStatus
                typeof(int),        // EducationLevel
                typeof(DateTime),   // BirthDate
                typeof(string),     // Phone
                typeof(string),     // Email
                typeof(DateTime),   // CreatedAt
                typeof(DateTime?),  // UpdatedAt
            },
            map: (item) =>
            {
                Employee? employee = item[0] as Employee;

                employee?.SetDocument((string)item[1])
                         .SetName((string)item[2], (string)item[3])
                         .SetSex((string)item[4])
                         .SetMaritalStatus((int)item[5])
                         .SetEducationLevel((int)item[6])
                         .SetBirthDate((DateTime)item[7])
                         .SetPhone((string)item[8])
                         .SetEmail((string)item[9])
                         .SetCreatedAt((DateTime)item[10])
                         .SetUpdatedAt((DateTime?)item[11]);

                employee?.CleanNotifications();

                return employee;
            },
            splitOn: "Document,FullName,SocialName,Sex,MaritalStatus,EducationLevel,BirthDate,Phone,Email,CreatedAt,UpdatedAt"
        );
    }

    public async Task<Employee?> GetById(string id)
    {
        (Type[] types, Func<object[], Employee?> map, string splitOn) = MapToEntity();

        var result = (await _adapter.GetConnection().QueryAsync<Employee?>(
            sql: EmployeeQuery.QueryGetById,
            types: types,
            map: map,
            splitOn: splitOn,
            param: new { Id = id }
        ));

        return result == null
            ? null
            : result?.FirstOrDefault();
    }

    public async Task<Employee?> GetByDocument(string document)
    {
        (Type[] types, Func<object[], Employee?> map, string splitOn) = MapToEntity();

        var result = await _adapter.GetConnection().QueryAsync<Employee?>(
            sql: EmployeeQuery.QueryGetByDocument,
            types: types,
            map: map,
            splitOn: splitOn,
            param: new { Document = document }
        );

        return result == null
            ? null
            : result?.FirstOrDefault();
    }

    public async Task<IEnumerable<EmployeeDto>> GetAll(int page, int pageSize = 10)
    {
        page = page < 1 ? 1 : page;

        int offSet = (page - 1) * pageSize;

        var results = await _adapter.QueryAsync<EmployeeDto>(
            EmployeeQuery.QueryGetAll,
            new
            {
                Next = pageSize,
                Offset = offSet
            }
        );

        return results;
    }
}
