using EmployeeSample.Application.Interfaces.Queries;

namespace EmployeeSample.Application.Features.Employees.Queries.FindAllEmployee;

public class FindAllEmployeeQuery : IQuery
{
    public int Page { get; set; } = 1;
    public int Limit { get; set; } = 10;
}
