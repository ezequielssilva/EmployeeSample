using EmployeeSample.Application.Interfaces.Commands;

namespace EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommandResult : ICommandResult
{
    public string Id { get; set; } = default!;

    public DeleteEmployeeCommandResult(string id)
    {
        Id = id;
    }
}
