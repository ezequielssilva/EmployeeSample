using EmployeeSample.Application.Interfaces.Commands;

namespace EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand : ICommand
{
    public string Document { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string SocialName { get; set; } = default!;
    public string Sex { get; set; } = default!;
    public int? MaritalStatus { get; set; }
    public int? EducationLevel { get; set; }
    public string BirthDate { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
}
