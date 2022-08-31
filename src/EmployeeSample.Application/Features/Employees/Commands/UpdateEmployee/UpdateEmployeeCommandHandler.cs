using EmployeeSample.Application.Extensions;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Exceptions;

namespace EmployeeSample.Application.Features.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeCommandHandler : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeCommandResult>
{
    private readonly IWriteEmployeeRepository _writeRepository;
    private readonly IReadEmployeeRepository _readRepository;

    public UpdateEmployeeCommandHandler(
        IWriteEmployeeRepository writeRepository,
        IReadEmployeeRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<UpdateEmployeeCommandResult> Handle(UpdateEmployeeCommand command)
    {
        await command.ValidateAsync(new UpdateEmployeeCommandValidator());

        Employee? employee = await _readRepository.GetById(command.Id);
        if (employee == null)
            throw new EntityNotFoundException("Employee not found");

        employee.ChangeEmployee(
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

        bool documentExists = await _readRepository.CheckDocumentExists(employee.Id.ToString(), employee.Document.ToString());
        if (documentExists)
            throw new ApplicationValidationException("Document", "Document ready exists");

        await _writeRepository.Update(employee);

        return new UpdateEmployeeCommandResult(employee);
    }
}
