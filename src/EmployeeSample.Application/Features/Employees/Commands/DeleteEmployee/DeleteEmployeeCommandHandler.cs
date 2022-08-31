using EmployeeSample.Application.Extensions;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Exceptions;

namespace EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult>
{
    private readonly IWriteEmployeeRepository _writeRepository;
    private readonly IReadEmployeeRepository _readRepository;

    public DeleteEmployeeCommandHandler(
        IWriteEmployeeRepository writeRepository,
        IReadEmployeeRepository readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<DeleteEmployeeCommandResult> Handle(DeleteEmployeeCommand command)
    {
        await command.ValidateAsync(new DeleteEmployeeCommandValidator());

        if (!(await _readRepository.CheckExistsById(command.Id)))
            throw new EntityNotFoundException("Employee not found");

        await _writeRepository.Delete(command.Id);

        return new DeleteEmployeeCommandResult(command.Id);
    }
}
