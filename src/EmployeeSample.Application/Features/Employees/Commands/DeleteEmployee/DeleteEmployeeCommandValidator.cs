using FluentValidation;

namespace EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmpty();
    }
}
