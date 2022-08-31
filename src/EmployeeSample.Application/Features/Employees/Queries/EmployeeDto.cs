using EmployeeSample.Domain.Entities;

namespace EmployeeSample.Application.Features.Employees.Queries;

public class EmployeeDto
{
    public Guid Id { get; set; } = default!;
    public string Document { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string SocialName { get; set; } = default!;
    public string Sex { get; set; } = default!;
    public string MaritalStatus { get; set; } = default!;
    public string EducationLevel { get; set; } = default!;
    public string BirthDate { get; set; } = default!;
    public string Phone { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string CreatedAt { get; set; } = default!;
    public string? UpdatedAt { get; set; } = default!;

    public EmployeeDto MapFrom(Employee employee)
    {
        Id = employee.Id;
        Document = employee.Document.ToString();
        FullName = employee.Name.FullName;
        SocialName = employee.Name.SocialName;
        Sex = employee.Sex.ToString();
        MaritalStatus = employee.MaritalStatus.ToString();
        EducationLevel = employee.EducationLevel.ToString();
        BirthDate = employee.BirthDate.ToString();
        Phone = employee.Phone.ToString();
        Email = employee.Email.ToString();
        CreatedAt = employee.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
        UpdatedAt = employee.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss");
        return this;
    }
}
