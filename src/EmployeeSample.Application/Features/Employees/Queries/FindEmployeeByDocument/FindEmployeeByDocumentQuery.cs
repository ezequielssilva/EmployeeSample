using EmployeeSample.Application.Interfaces.Queries;

namespace EmployeeSample.Application.Features.Employees.Queries.FindEmployeeByDocument;

public class FindEmployeeByDocumentQuery : IQuery
{
    public string Document { get; set; } = default!;
}
