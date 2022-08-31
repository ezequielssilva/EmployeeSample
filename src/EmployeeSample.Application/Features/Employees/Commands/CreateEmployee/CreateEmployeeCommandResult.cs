using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommandResult : ICommandResult
{
    public string Id { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public CreateEmployeeCommandResult(Employee employee)
    {
        this.Id = employee.Id.ToString();
        this.CreatedAt = employee.CreatedAt;
    }
}
