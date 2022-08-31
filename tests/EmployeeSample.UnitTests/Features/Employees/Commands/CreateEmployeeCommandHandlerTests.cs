using EmployeeSample.Application.Features.Employees.Commands.CreateEmployee;
using EmployeeSample.Application.Interfaces.ReadRepositories;
using EmployeeSample.Application.Interfaces.WriteRepositories;
using EmployeeSample.Domain.Entities;
using EmployeeSample.Domain.Exceptions;
using Moq;

namespace EmployeeSample.UnitTests.Features.Employees.Commands;

public class CreateEmployeeCommandHandlerTests
{
    private readonly Mock<IWriteEmployeeRepository> _writeRepositoryMock;
    private readonly Mock<IReadEmployeeRepository> _readRepositoryMock;
    private readonly CreateEmployeeCommandHandler _handler;

    public CreateEmployeeCommandHandlerTests()
    {
        _writeRepositoryMock = new Mock<IWriteEmployeeRepository>();
        _readRepositoryMock = new Mock<IReadEmployeeRepository>();
        IWriteEmployeeRepository writeRepository = _writeRepositoryMock.Object;
        IReadEmployeeRepository readRepository = _readRepositoryMock.Object;
        _handler = new CreateEmployeeCommandHandler(writeRepository, readRepository);
    }

    [Fact]
    public async void SuccessWhenCreateEmployee()
    {
        // Arrange
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Document = "32928190082",
            FullName = "Test Employee Fullname",
            BirthDate = "1990-06-30",
            EducationLevel = 0,
            MaritalStatus = 1,
            Sex = "M",
            Email = "testemployee@email.com",
            Phone = "0091122334455"
        };
        _readRepositoryMock.Setup(x => x.CheckDocumentExists(It.IsAny<string>())).Returns(Task.FromResult(false));

        // Act
        var result = await _handler.Handle(command);

        // Assert
        _writeRepositoryMock.Verify(x => x.Insert(It.IsAny<Employee>()), Times.Once);
        Assert.NotNull(result);
        Assert.NotNull(result.Id);
        Assert.True(Guid.TryParse(result.Id, out _));
    }

    [Fact]
    public void ErrorWhenCreateEmployeeCommandIsInvalid_ThrowValidatorException()
    {
        // Arrange
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Document = "",
            FullName = "",
            BirthDate = "",
            EducationLevel = null,
            MaritalStatus = null,
            Sex = "",
            Email = "",
            Phone = ""
        };

        // Act
        var act = () => _handler.Handle(command);

        // Assert
        var exception = Assert.ThrowsAsync<ValidatorException>(act);
        _writeRepositoryMock.Verify(x => x.Insert(It.IsAny<Employee>()), Times.Never);
    }

    [Fact]
    public async void ErrorWhenCreateEmployeeWithDocumentReadyExists_ThrowApplicationValidationException()
    {
        // Arrange
        CreateEmployeeCommand command = new CreateEmployeeCommand
        {
            Document = "32928190082",
            FullName = "Test Employee Fullname",
            BirthDate = "1990-06-30",
            EducationLevel = 0,
            MaritalStatus = 1,
            Sex = "M",
            Email = "testemployee@email.com",
            Phone = "0091122334455"
        };
        _readRepositoryMock.Setup(x => x.CheckDocumentExists(It.IsAny<string>())).Returns(Task.FromResult(true));

        // Act
        var act = () => _handler.Handle(command);

        // Assert
        var exception = await Assert.ThrowsAsync<ApplicationValidationException>(act);
        _writeRepositoryMock.Verify(x => x.Insert(It.IsAny<Employee>()), Times.Never);
        Assert.Equal("Document ready exists", exception.Message);
    }
}
