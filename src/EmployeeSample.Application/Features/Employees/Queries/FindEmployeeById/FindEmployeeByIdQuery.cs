using EmployeeSample.Application.Interfaces.Queries;

namespace EmployeeSample.Application.Features.Employees.Queries.FindEmployeeById;

public record FindEmployeeByIdQuery : IQuery
{
    public string Id { get; set; } = default!;
}
