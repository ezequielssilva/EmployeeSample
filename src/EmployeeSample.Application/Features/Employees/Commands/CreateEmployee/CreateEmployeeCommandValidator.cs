using FluentValidation;

namespace EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(command => command.Document)
            .NotEmpty()
            .MinimumLength(11)
            .MaximumLength(11);

        RuleFor(command => command.FullName)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(60);

        RuleFor(command => command.SocialName)
            .MaximumLength(60);

        RuleFor(command => command.Sex)
            .NotEmpty()
            .Length(1);

        RuleFor(command => command.MaritalStatus)
            .NotEmpty();

        RuleFor(command => command.EducationLevel)
            .NotEmpty();

        RuleFor(command => command.BirthDate)
            .NotEmpty();

        RuleFor(command => command.Phone)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(20);

        RuleFor(command => command.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(120)
            .EmailAddress();
    }
}
