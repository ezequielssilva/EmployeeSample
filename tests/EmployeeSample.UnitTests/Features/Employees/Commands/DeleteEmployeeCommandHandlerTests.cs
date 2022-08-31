using EmployeeSample.Application.Features.Employees.Commands.DeleteEmployee;
using EmployeeSample.Application.Interfaces.Commands;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using Moq;

namespace EmployeeSample.UnitTests.Features.Employees.Commands;

public class DeleteEmployeeCommandHandlerTests
{
    private readonly Mock<IWriteEmployeeRepository> _writeRepositoryMock;
    private readonly Mock<IReadEmployeeRepository> _readRepositoryMock;
    private readonly ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeCommandResult> _handler;

    public DeleteEmployeeCommandHandlerTests()
    {
        _writeRepositoryMock = new Mock<IWriteEmployeeRepository>();
        _readRepositoryMock = new Mock<IReadEmployeeRepository>();
        IWriteEmployeeRepository writeRepository = _writeRepositoryMock.Object;
        IReadEmployeeRepository readRepository = _readRepositoryMock.Object;
        _handler = new DeleteEmployeeCommandHandler(writeRepository, readRepository);
    }

    [Fact]
    public async void SuccessWhenEmployeeExists()
    {
        // Arrange
        Guid employeeId = Guid.NewGuid();

        DeleteEmployeeCommand command = new DeleteEmployeeCommand
        {
            Id = employeeId.ToString()
        };

        _readRepositoryMock.Setup(x => x.CheckExistsById(It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var result = await _handler.Handle(command);

        // Assert
        _writeRepositoryMock.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        Assert.Equal(employeeId.ToString(), result.Id);
    }
}
