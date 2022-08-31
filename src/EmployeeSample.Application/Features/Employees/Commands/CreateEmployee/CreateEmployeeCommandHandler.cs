using EmployeeSample.Application.Extensions;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Exceptions;

namespace EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;

public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, CreateEmployeeCommandResult>
{
    private readonly IWriteEmployeeRepository _writeRepository;
    private readonly IReadEmployeeRepository _readRepository;

    public CreateEmployeeCommandHandler(
        IWriteEmployeeRepository writeRepository,
        IReadEmployeeRepository readRepository
    )
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<CreateEmployeeCommandResult> Handle(CreateEmployeeCommand command)
    {
        await command.ValidateAsync(new CreateEmployeeCommandValidator());

        var employee = new Employee(
            document: command.Document,
            fullName: command.FullName,
            socialName: command.SocialName,
            sex: command.Sex,
            maritalStatus: command.MaritalStatus,
            educationLevel: command.EducationLevel,
            birthDate: command.BirthDate,
            phone: command.Phone,
            email: command.Email
        );

        employee.DomainValidate();

        bool documentExists = await _readRepository.CheckDocumentExists(employee.Document.ToString());
        if (documentExists)
            throw new ApplicationValidationException("Document", "Document ready exists");

        await _writeRepository.Insert(employee);

        return new CreateEmployeeCommandResult(employee);
    }
}
