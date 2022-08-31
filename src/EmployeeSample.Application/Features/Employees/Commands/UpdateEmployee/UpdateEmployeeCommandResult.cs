using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandResult : ICommandResult
{
    public string Id { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public UpdateEmployeeCommandResult(Employee employee)
    {
        this.Id = employee.Id.ToString();
        this.UpdatedAt = employee.UpdatedAt;
    }
}
