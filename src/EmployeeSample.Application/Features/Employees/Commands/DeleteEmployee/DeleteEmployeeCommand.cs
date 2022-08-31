using EmployeeSample.Application.Interfaces.Commands;

namespace EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand : ICommand
{
    public string Id { get; set; } = default!;
}
